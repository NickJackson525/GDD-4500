using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shield_Animation_Transition : NetworkBehaviour
{
    #region Variables

    public GameObject Shield_Close;

    [SyncVar]
    public GameObject carToFollow;

    GameObject createdShield;

    #endregion

    #region Start

    // Use this for initialization
    void Start ()
    {
		
	}

    #endregion

    #region Update

    // Update is called once per frame
    void Update ()
    {
        //timer--;

        //if (timer == 0)
        //{
        //    createdShield = Instantiate(Shield_Close, transform.position, transform.rotation);
        //    createdShield.GetComponent<Shield_Animation_Close>().carToFollow = this.carToFollow;
        //    Destroy(this.gameObject);
        //}

        this.transform.position = new Vector3(carToFollow.transform.position.x, carToFollow.transform.position.y, -1);
        this.transform.rotation = carToFollow.transform.rotation;
        this.transform.Rotate(0, 0, 180);
    }

    #endregion

    #region Custom Methods

    [Command]
    void CmdNextState()
    {
        createdShield = Instantiate(Shield_Close, transform.position, transform.rotation);
        createdShield.GetComponent<Shield_Animation_Close>().carToFollow = this.carToFollow;
        NetworkServer.Spawn(createdShield);
        Destroy(this.gameObject);
    }

    #endregion

    #region Collision Methods

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Contains("Racetrack") || coll.gameObject.tag.Contains("Powerup"))
        {
            CmdNextState();
        }

        if (coll.gameObject.tag.Contains("Player") && (coll.gameObject.tag != carToFollow.tag))
        {
            coll.gameObject.GetComponent<Car_Controller>().health -= 20;
            coll.gameObject.GetComponent<Car_Controller>().collisionTimer = 60;

            if (coll.gameObject.GetComponent<Car_Controller>().health <= 0)
            {
                coll.gameObject.GetComponent<Car_Controller>().LostGame();
            }

            CmdNextState();
        }
    }

    #endregion
}
