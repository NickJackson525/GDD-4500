﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Car_Controller : NetworkBehaviour
{
    #region Variables

    [SyncVar]
    public int health = 100;

    public Game_Manager.Pickup currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
    public bool hasPickup = false;
    GameObject createdIcon;
    GameObject createdPickup;
    public GameObject kittenCannon;
    public GameObject kittenFollowPrefab;
    public GameObject shield;
    public GameObject fakePedestrian;
    public GameObject harpoon;
    public GameObject UICanvas;
    public GameObject kittenIcon;
    public GameObject shieldIcon;
    public GameObject harpoonIcon;
    public GameObject fakePedestrianIcon;
    public GameObject startEndController;
    GameObject startLight;
    GameObject temp;
    float speed = 17f;
    float turnPower = -110f;
    float driftPower = 0.95f;
    public int checkpointsPassed = 0;
    public int collisionTimer = 0;
    public bool hasBomb = false;
    public bool hasShield = false;
    public bool canMove = true;
    bool canCollide = true;
    Quaternion initialRotation;
    Quaternion tempRotation;
    Rigidbody2D rb;

    #endregion

    #region Start

    public override void OnStartServer()
    {
        if (!Game_Manager.Instance.startEnd)
        {
            GameObject newObject = Instantiate(startEndController);
            NetworkServer.Spawn(newObject);
            Game_Manager.Instance.startEnd = newObject.GetComponent<StartEnd>();
        }
    }

    public override void OnStartLocalPlayer()
    {
        Game_Manager.Instance.player = gameObject;
    }

    // Use this for initialization
    void Start()
    {
        if(!localPlayerAuthority)
        {
            return;
        }

        startLight = GameObject.FindGameObjectWithTag("StartLight");
        initialRotation = transform.rotation;
        UICanvas = GameObject.FindGameObjectWithTag("p1Canvas");
        rb = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Updates

    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CmdStartGame();
            }

            if (!Game_Manager.Instance.startEnd)
            {
                Game_Manager.Instance.startEnd = GameObject.Find("StartEndController(Clone)").GetComponent<StartEnd>();
            }

            #region Player 1 Health

            Game_Manager.Instance.playerHealth[0].transform.position = transform.position;
            Game_Manager.Instance.playerHealth[0].transform.rotation = transform.rotation;
            Game_Manager.Instance.playerHealth[1].transform.position = transform.position;
            Game_Manager.Instance.playerHealth[1].transform.rotation = transform.rotation;

            if (Game_Manager.Instance.playerHealth[0].name == "HealthBar")
            {
                Game_Manager.Instance.playerHealth[0].GetComponent<Image>().fillAmount = (float)health / 100f;

                if (health <= 25)
                {
                    Game_Manager.Instance.playerHealth[0].GetComponent<Image>().color = Color.red;
                }
                else if (health <= 50)
                {
                    Game_Manager.Instance.playerHealth[0].GetComponent<Image>().color = Color.yellow;
                }
            }
            else
            {
                Game_Manager.Instance.playerHealth[1].GetComponent<Image>().fillAmount = (float)health / 100f;

                if (health <= 25)
                {
                    Game_Manager.Instance.playerHealth[1].GetComponent<Image>().color = Color.red;
                }
                else if (health <= 50)
                {
                    Game_Manager.Instance.playerHealth[1].GetComponent<Image>().color = Color.yellow;
                }
            }

            #endregion
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            rb.velocity = getForewordVelocity(rb) + getSidewaysVelocity(rb) * driftPower;

            if ((startLight.activeSelf == false) && canMove)
            {
                #region Player Controls

                if (Input.GetKey(KeyCode.W))
                {
                    rb.AddForce(transform.up * speed);
                }

                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(transform.up * (-speed / 2));
                }

                if (Input.GetKey(KeyCode.A))
                {
                    rb.angularVelocity = -turnPower;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    rb.angularVelocity = turnPower;
                }

                if (hasPickup && Input.GetKeyUp(KeyCode.Space))
                {
                    CmdUsePickup(currentPickup);
                    hasPickup = false;
                }

                #endregion
            }

            if (collisionTimer > 0)
            {
                collisionTimer--;
            }
            else
            {
                canCollide = true;
            }
        }
    }

    #endregion

    #region Custom Methods

    Vector2 getForewordVelocity(Rigidbody2D rb)
    {
        return transform.up * Vector2.Dot(rb.velocity, transform.up);
    }

    Vector2 getSidewaysVelocity(Rigidbody2D rb)
    {
        return transform.right * Vector2.Dot(rb.velocity, transform.right);
    }

    public void LostGame()
    {
        Game_Manager.Instance.GameOver(this.gameObject, false, UICanvas);
    }

    public void SpawnPickupIcon()
    {
        if (isLocalPlayer)
        {
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                    createdIcon = Instantiate(fakePedestrianIcon, transform.position, initialRotation);
                    break;
                case 1:
                    currentPickup = Game_Manager.Pickup.HARPOON;
                    createdIcon = Instantiate(harpoonIcon, transform.position, initialRotation);
                    break;
                case 2:
                    currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
                    createdIcon = Instantiate(kittenIcon, transform.position, initialRotation);
                    break;
                case 3:
                    currentPickup = Game_Manager.Pickup.SHIELD;
                    createdIcon = Instantiate(shieldIcon, transform.position, initialRotation);
                    break;
                default:
                    currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                    createdIcon = Instantiate(fakePedestrianIcon, transform.position, initialRotation);
                    break;
            }

            createdIcon.GetComponent<Pickup_Icon>().followTarget = gameObject;
            hasPickup = true;
        }
    }

    [Command]
    void CmdUsePickup(Game_Manager.Pickup pickup)
    {
        switch (pickup)
        {
            case Game_Manager.Pickup.FAKE_PEDESTRIAN:
                tempRotation = transform.rotation;
                transform.rotation = initialRotation;
                createdPickup = Instantiate(fakePedestrian, new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z), transform.rotation);
                transform.rotation = tempRotation;
                break;
            case Game_Manager.Pickup.HARPOON:
                tempRotation = transform.rotation;
                transform.rotation = initialRotation;
                createdPickup = Instantiate(harpoon, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), transform.rotation, transform);
                createdPickup.GetComponent<Harpoon>().playerStart = this.gameObject;
                createdPickup.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                transform.rotation = tempRotation;
                break;
            case Game_Manager.Pickup.KITTEN_CANNON:
                tempRotation = transform.rotation;
                transform.rotation = initialRotation;
                createdPickup = Instantiate(kittenCannon, new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), transform.rotation, transform);
                createdPickup.GetComponent<Kitten_Cannon>().playerStart = this.gameObject;
                transform.rotation = tempRotation;
                break;
            case Game_Manager.Pickup.SHIELD:
                createdPickup = Instantiate(shield, new Vector3(transform.position.x, transform.position.y - 1.63f, transform.position.z), transform.rotation, transform);
                createdPickup.GetComponent<Shield_Animation_Create>().carToFollow = this.gameObject;
                break;
            default:
                break;
        }

        NetworkServer.Spawn(createdPickup);
    }

    [Command]
    public void CmdCreateKittenFollow()
    {
        if (!localPlayerAuthority)
        {
            return;
        }

        temp = Instantiate(kittenFollowPrefab);
        temp.GetComponent<Kitten_Follow>().followTarget = gameObject;

        if (gameObject.tag.Contains("Player1"))
        {
            temp.gameObject.layer = 8;
        }
        else if (gameObject.tag.Contains("Player2"))
        {
            temp.gameObject.layer = 9;
        }

        NetworkServer.Spawn(temp);
    }

    [Command]
    public void CmdStartGame()
    {
        Game_Manager.Instance.startEnd.startGame = true;
    }

    #endregion

    #region Collision Methods

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(((coll.gameObject.tag.Contains("Player")) || (coll.gameObject.tag.Contains("Racetrack"))) && canCollide)
        {
            health -= 10;
            collisionTimer = 60;
            canCollide = false;

            if (health <= 0)
            {
                Game_Manager.Instance.GameOver(this.gameObject, false, UICanvas);
            }
        }
    }

    #endregion
}
