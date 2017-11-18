using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kitten_Cannon : NetworkBehaviour
{
    #region Variables

    [SyncVar]
    public GameObject playerStart;

    public GameObject kittenFollowPrefab;
    GameObject temp;
    public Rigidbody2D rb;
    public float speed = 12f;
    int timer = 100;

    #endregion

    #region Start

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}

    #endregion

    #region Update

    // Update is called once per frame
    void Update ()
    {
        timer--;

        rb.AddForce(-transform.up * speed);

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region Custom Methods


    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag.Contains("Player")) && (coll.gameObject != playerStart))
        {
            coll.GetComponent<Car_Controller>().CreateKittenFollow();
            playerStart.GetComponent<Car_Controller>().score += 100;
            Destroy(this.gameObject);
        }
    }

    #endregion
}
