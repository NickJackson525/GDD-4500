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
    public Game_Manager.Pickup currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
    [SyncVar]
    public bool hasPickup = false;
    [SyncVar]
    public int health = 100;

    GameObject createdPickup;
    public GameObject kittenCannon;
    public GameObject kittenFollowPrefab;
    public GameObject shield;
    public GameObject fakePedestrian;
    public GameObject harpoon;
    public GameObject UICanvas;
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

        //CmdIncreasePlayerCount();

        //if (GameObject.FindGameObjectWithTag("Player1"))
        //{
        //    playerNumber = 2;
        //    gameObject.tag = "Player2";
        //    mainCamera.GetComponent<Camera_Follow>().playerNumToFollow = 2;
        //    mainCamera.GetComponent<Camera>().cullingMask ^= 1 << LayerMask.NameToLayer("Player 2");
        //}
        //else
        //{
        //    playerNumber = 1;
        //    gameObject.tag = "Player1";
        //    mainCamera.GetComponent<Camera_Follow>().playerNumToFollow = 1;
        //    mainCamera.GetComponent<Camera>().cullingMask ^= 1 << LayerMask.NameToLayer("Player 1");
        //}
    }

    #endregion

    #region Updates

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //if (playerNumber == 1)
        //{
            #region Player 1 Health

            Game_Manager.Instance.playerHealth[0].transform.position = transform.position;
            Game_Manager.Instance.playerHealth[0].transform.rotation = transform.rotation;
            Game_Manager.Instance.playerHealth[1].transform.position = transform.position;
            Game_Manager.Instance.playerHealth[1].transform.rotation = transform.rotation;

            if(Game_Manager.Instance.playerHealth[0].name == "HealthBar")
            {
                Game_Manager.Instance.playerHealth[0].GetComponent<Image>().fillAmount = (float)health / 100f;

                if(health <= 25)
                {
                    Game_Manager.Instance.playerHealth[0].GetComponent<Image>().color = Color.red;
                }
                else if(health <= 50)
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
        //}
        //else if (playerNumber == 2)
        //{
            #region Player 2 Health

        //    Game_Manager.Instance.p2Health[0].transform.position = transform.position;
        //    Game_Manager.Instance.p2Health[0].transform.rotation = transform.rotation;
        //    Game_Manager.Instance.p2Health[1].transform.position = transform.position;
        //    Game_Manager.Instance.p2Health[1].transform.rotation = transform.rotation;

        //    if (Game_Manager.Instance.p2Health[0].name == "HealthBar")
        //    {
        //        Game_Manager.Instance.p2Health[0].GetComponent<Image>().fillAmount = (float)health / 100f;

        //        if (health <= 25)
        //        {
        //            Game_Manager.Instance.p2Health[0].GetComponent<Image>().color = Color.red;
        //        }
        //        else if (health <= 50)
        //        {
        //            Game_Manager.Instance.p2Health[0].GetComponent<Image>().color = Color.yellow;
        //        }
        //    }
        //    else
        //    {
        //        Game_Manager.Instance.p2Health[1].GetComponent<Image>().fillAmount = (float)health / 100f;

        //        if (health <= 25)
        //        {
        //            Game_Manager.Instance.p2Health[1].GetComponent<Image>().color = Color.red;
        //        }
        //        else if (health <= 50)
        //        {
        //            Game_Manager.Instance.p2Health[1].GetComponent<Image>().color = Color.yellow;
        //        }
        //    }

            #endregion
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        rb.velocity = getForewordVelocity(rb) + getSidewaysVelocity(rb) * driftPower;

        if ((startLight.activeSelf == false) && canMove)
        {
            //if (playerNumber == 1)
            //{
                #region Player 1 Controls

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
                    CmdUsePickup();
                }

                #endregion
            //}
            //else if (playerNumber == 2)
            //{
                #region Player 2 Controls

            //    if (Input.GetKey(KeyCode.UpArrow))
            //    {
            //        rb.AddForce(transform.up * speed);
            //    }

            //    if (Input.GetKey(KeyCode.DownArrow))
            //    {
            //        rb.AddForce(transform.up * (-speed / 2));
            //    }

            //    if (Input.GetKey(KeyCode.LeftArrow))
            //    {
            //        rb.angularVelocity = -turnPower;
            //    }

            //    if (Input.GetKey(KeyCode.RightArrow))
            //    {
            //        rb.angularVelocity = turnPower;
            //    }

            //    if (hasPickup && Input.GetKeyUp(KeyCode.KeypadEnter))
            //    {
            //        CmdUsePickup();
            //    }

                #endregion
            //}
        }

        if(collisionTimer > 0)
        {
            collisionTimer--;
        }
        else
        {
            canCollide = true;
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

    [Command]
    void CmdUsePickup()
    {
        if (!localPlayerAuthority)
        {
            return;
        }

        switch (currentPickup)
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

        hasPickup = false;
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

    //[Command]
    //public void CmdIncreasePlayerCount()
    //{
    //    if(!localPlayerAuthority)
    //    {
    //        return;
    //    }

    //    if (Game_Manager.Instance.playerNumber > 1)
    //    {
    //        gameObject.tag = "Player" + Game_Manager.Instance.playerNumber;
    //        mainCamera.GetComponent<Camera_Follow>().playerNumToFollow = Game_Manager.Instance.playerNumber;
    //        mainCamera.GetComponent<Camera>().cullingMask ^= 1 << LayerMask.NameToLayer("Player " + Game_Manager.Instance.playerNumber);
    //        Game_Manager.Instance.player2 = gameObject;
    //        playerNumber = Game_Manager.Instance.playerNumber;
    //    }
    //    else
    //    {
    //        gameObject.tag = "Player" + Game_Manager.Instance.playerNumber;
    //        mainCamera.GetComponent<Camera_Follow>().playerNumToFollow = Game_Manager.Instance.playerNumber;
    //        mainCamera.GetComponent<Camera>().cullingMask ^= 1 << LayerMask.NameToLayer("Player " + Game_Manager.Instance.playerNumber);
    //        Game_Manager.Instance.player1 = gameObject;
    //        playerNumber = Game_Manager.Instance.playerNumber;
    //    }

    //    Game_Manager.Instance.playerNumber++;
    //}

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
