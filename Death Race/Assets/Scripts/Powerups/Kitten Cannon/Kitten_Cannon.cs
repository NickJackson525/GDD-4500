using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kitten_Cannon : NetworkBehaviour
{
    #region Variables

    public GameObject kittenFollowPrefab;
    GameObject temp;
    //public Vector3 directionToFire;
    public Rigidbody2D rb;
    public float speed = 12f;
    int timer = 100;
    //[SyncVar]
    //public GameObject collObject;
    [SyncVar]
    public GameObject playerStart;

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

    //[Command]
    //void CmdNextState()
    //{
    //    temp = Instantiate(kittenFollowPrefab);
    //    temp.GetComponent<Kitten_Follow>().followTarget = collObject;

    //    if (collObject.tag.Contains("Player1"))
    //    {
    //        temp.gameObject.layer = 8;
    //    }
    //    else if (collObject.tag.Contains("Player2"))
    //    {
    //        temp.gameObject.layer = 9;
    //    }

    //    NetworkServer.Spawn(temp);
    //    NetworkServer.Destroy(this.gameObject);
    //}

    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag.Contains("Player")) && (coll.gameObject.tag != playerStart.tag))
        {
            //collObject = coll.gameObject;
            //CmdNextState();

            coll.GetComponent<Car_Controller>().CmdCreateKittenFollow();
            Destroy(this.gameObject);
        }
    }

    #endregion
}
