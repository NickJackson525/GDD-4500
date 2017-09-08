using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    #region Variables

    float speed = 10f;
    float turnPower = -85f;
    public int playerNumber;
    public bool hasBomb = false;
    public bool hasShield = false;
    public bool canMove = true;
    public GameObject bombFollow;
    GameObject startLight;
    public int checkpointsPassed = 0;

    #endregion

    #region Start

    // Use this for initialization
    void Start()
    {
        startLight = GameObject.FindGameObjectWithTag("StartLight");
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

                if (hasBomb && Input.GetKeyDown(KeyCode.Space))
                {
                    hasBomb = false;
                    bombFollow.GetComponent<Bomb_Drop>().Drop_Bomb();
                }
            }
            else if (playerNumber == 2)
            {
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

                if (hasBomb && Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    hasBomb = false;
                    bombFollow.GetComponent<Bomb_Drop>().Drop_Bomb();
                }

                //if (Input.GetButton("Accelerate"))
                //{
                //    rb.AddForce(transform.up * speed);
                //}

                //if (Input.GetButton("Brake"))
                //{
                //    rb.AddForce(transform.up * (-speed / 2));
                //}

                //rb.angularVelocity = Input.GetAxis("Horizontal") * turnPower;
            }
        }
    }

    #endregion

    #region Methods

    Vector2 getForewordVelocity(Rigidbody2D rb)
    {
        return transform.up * Vector2.Dot(rb.velocity, transform.up);
    }

    Vector2 getSidewaysVelocity(Rigidbody2D rb)
    {
        return transform.right * Vector2.Dot(rb.velocity, transform.right);
    }

    #endregion
}
