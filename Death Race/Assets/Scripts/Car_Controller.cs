using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Car_Controller : MonoBehaviour
{
    #region Variables

    public Game_Manager.Pickup currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
    public GameObject kittenCannon;
    public GameObject shield;
    public GameObject p1Canvas;
    public GameObject p2Canvas;
    GameObject startLight;
    GameObject createdPickup;
    float speed = 10f;
    float turnPower = -85f;
    public int playerNumber;
    public int checkpointsPassed = 0;
    public int health = 100;
    int collisionTimer = 0;
    public bool hasBomb = false;
    public bool hasShield = false;
    public bool canMove = true;
    public bool hasPickup = false;
    bool canCollide = true;
    Quaternion initialRotation;
    Quaternion tempRotation;
    
    #endregion

    #region Start

    // Use this for initialization
    void Start()
    {
        startLight = GameObject.FindGameObjectWithTag("StartLight");
        initialRotation = transform.rotation;
    }

    #endregion

    #region Fixed Update

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = getForewordVelocity(rb);

        if ((startLight.activeSelf == false) && canMove)
        {
            if (playerNumber == 1)
            {
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
                    switch(currentPickup)
                    {
                        case Game_Manager.Pickup.FAKE_PEDESTRIAN:
                            break;
                        case Game_Manager.Pickup.HARPOON:
                            break;
                        case Game_Manager.Pickup.KITTEN_CANNON:
                            tempRotation = transform.rotation;
                            transform.rotation = initialRotation;
                            Instantiate(kittenCannon, new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), transform.rotation, transform);
                            transform.rotation = tempRotation;
                            hasPickup = false;
                            break;
                        case Game_Manager.Pickup.SHIELD:
                            createdPickup = Instantiate(shield, new Vector3(transform.position.x, transform.position.y - 1.63f, transform.position.z), transform.rotation, transform);
                            createdPickup.GetComponent<Shield_Animation_Create>().carToFollow = this.gameObject;
                            hasPickup = false;
                            break;
                        default:
                            break;
                    }
                }

                #endregion
            }
            else if (playerNumber == 2)
            {
                #region Player 2 Controls

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    rb.AddForce(transform.up * speed);
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    rb.AddForce(transform.up * (-speed / 2));
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    rb.angularVelocity = -turnPower;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    rb.angularVelocity = turnPower;
                }

                if (hasPickup && Input.GetKeyUp(KeyCode.KeypadEnter))
                {
                    switch (currentPickup)
                    {
                        case Game_Manager.Pickup.FAKE_PEDESTRIAN:
                            break;
                        case Game_Manager.Pickup.HARPOON:
                            break;
                        case Game_Manager.Pickup.KITTEN_CANNON:
                            tempRotation = transform.rotation;
                            transform.rotation = initialRotation;
                            Instantiate(kittenCannon, new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), transform.rotation, transform);
                            transform.rotation = tempRotation;
                            hasPickup = false;
                            break;
                        case Game_Manager.Pickup.SHIELD:
                            createdPickup = Instantiate(shield, new Vector3(transform.position.x, transform.position.y - 1.63f, transform.position.z), transform.rotation, transform);
                            createdPickup.GetComponent<Shield_Animation_Create>().carToFollow = this.gameObject;
                            hasPickup = false;
                            break;
                        default:
                            break;
                    }
                }

                #endregion
            }
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
                Game_Manager.Instance.GameOver(this.gameObject, false, p1Canvas, p2Canvas);
            }
        }

        if(coll.gameObject.tag == "Shield")
        {
            health -= 20;
            collisionTimer = 60;
            Destroy(coll.gameObject);

            if (health <= 0)
            {
                Game_Manager.Instance.GameOver(this.gameObject, false, p1Canvas, p2Canvas);
            }
        }
    }

    #endregion
}
