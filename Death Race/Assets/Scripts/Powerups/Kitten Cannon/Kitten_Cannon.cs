using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kitten_Cannon : NetworkBehaviour
{
    #region Variables

    public GameObject kittenFollow;
    GameObject temp;
    public Vector3 directionToFire;
    public Rigidbody2D rb;
    public float speed = 12f;
    int timer = 100;
    Collider2D collObject;

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

    [Command]
    void CmdNextState()
    {
        temp = Instantiate(kittenFollow);
        temp.GetComponent<Kitten_Follow>().followTarget = collObject.gameObject;

        if (collObject.gameObject.tag.Contains("Player1"))
        {
            temp.gameObject.layer = 8;
        }
        else if (collObject.gameObject.tag.Contains("Player2"))
        {
            temp.gameObject.layer = 9;
        }

        NetworkServer.Spawn(temp);
        Destroy(this.gameObject);
    }

    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D coll)
    {
        collObject = coll;

        if (coll.tag.Contains("Player"))
        {
            CmdNextState();
        }
    }

    #endregion
}
