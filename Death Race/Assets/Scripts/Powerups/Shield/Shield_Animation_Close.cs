using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shield_Animation_Close : NetworkBehaviour
{
    #region Variables

    [SyncVar]
    public GameObject carToFollow;

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

        if (currentSprite == "Shield_19")
        {
            carToFollow.GetComponent<Car_Controller>().hasPickup = false;
            Destroy(this.gameObject);
        }
        
        this.transform.position = new Vector3(carToFollow.transform.position.x, carToFollow.transform.position.y, -1);
        this.transform.rotation = carToFollow.transform.rotation;
        this.transform.Rotate(0, 0, 180);
    }

    #endregion

    #region Collision Methods

    #endregion
}
