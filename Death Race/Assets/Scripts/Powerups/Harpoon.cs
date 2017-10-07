using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Harpoon : NetworkBehaviour
{
    [SyncVar]
    public GameObject playerStart;
    [SyncVar]
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
                Destroy(this.gameObject);
            }

            playerToPush.GetComponent<Rigidbody2D>().velocity = rb.velocity;
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
            timer = 60;
        }

        if (coll.tag.Contains("Racetrack"))
        {
            Destroy(this.gameObject);
        }
    }
}
