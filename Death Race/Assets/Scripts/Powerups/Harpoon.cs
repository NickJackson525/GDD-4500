using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    public GameObject playerStart;
    GameObject playerToPush;
    public Rigidbody2D rb;
    public float speed = 12f;
    int timer = 0;
    bool hitPlayer = false;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (hitPlayer)
        {
            if (timer > 0)
            {
                timer--;
            }
            else
            {
                playerToPush.GetComponent<Rigidbody2D>().freezeRotation = false;
                Destroy(this.gameObject);
            }

            playerToPush.GetComponent<Rigidbody2D>().velocity = rb.velocity;
            playerToPush.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        rb.freezeRotation = true;
        rb.AddForce(transform.up * speed);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag.Contains("Player")) && (!hitPlayer) && (coll.gameObject.tag != playerStart.tag))
        {
            playerToPush = coll.gameObject;
            hitPlayer = true;
            timer = 90;
        }

        if (coll.tag.Contains("Racetrack"))
        {
            if(playerToPush != null)
            {
                playerToPush.GetComponent<Rigidbody2D>().freezeRotation = false;
            }

            Destroy(this.gameObject);
        }
    }
}
