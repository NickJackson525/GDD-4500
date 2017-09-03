using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    #region Variables

    float speed = 10f;
    float turnPower = -100f;
    public int playerNumber;

    #endregion

    #region Start

    // Use this for initialization
    void Start()
    {

    }

    #endregion

    #region Fixed Update

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = getForewordVelocity(rb);

        if(playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(transform.up * speed);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                rb.AddForce(transform.up * (-speed / 2));
            }

            if(Input.GetKey(KeyCode.A))
            {
                rb.angularVelocity = -turnPower;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rb.angularVelocity = turnPower;
            }
        }
        else if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                rb.AddForce(transform.up * speed);
            }

            if (Input.GetKey(KeyCode.RightControl))
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
