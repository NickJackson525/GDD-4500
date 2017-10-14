using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shield_Animation_Create : NetworkBehaviour
{
    #region Variables

    [SyncVar]
    public GameObject carToFollow;

    public GameObject Shield_Full;
    GameObject createdShield;
    string currentSprite;

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
        currentSprite = GetComponent<SpriteRenderer>().sprite.name;

        if (currentSprite == "Shield_10")
        {
            CmdNextState();
        }

        this.transform.position = new Vector3(carToFollow.transform.position.x, carToFollow.transform.position.y, -1);
        this.transform.rotation = carToFollow.transform.rotation;
        this.transform.Rotate(0, 0, 180);
    }

    #endregion

    #region Custom Methods

    [Command]
    void CmdNextState()
    {
        createdShield = Instantiate(Shield_Full, transform.position, transform.rotation);
        createdShield.GetComponent<Shield_Animation_Transition>().carToFollow = this.carToFollow;
        NetworkServer.Spawn(createdShield);
        Destroy(this.gameObject);
    }

    #endregion

    #region Collision Methods

    #endregion
}
