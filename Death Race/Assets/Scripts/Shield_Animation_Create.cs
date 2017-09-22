using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Animation_Create : MonoBehaviour
{
    #region Variables

    public GameObject Shield_Full;
    public GameObject carToFollow;
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
            createdShield = Instantiate(Shield_Full, transform.position, transform.rotation);
            createdShield.GetComponent<Shield_Animation_Transition>().carToFollow = this.carToFollow;
            Destroy(this.gameObject);
        }

        this.transform.position = new Vector3(carToFollow.transform.position.x, carToFollow.transform.position.y, -1);
        this.transform.rotation = carToFollow.transform.rotation;
        this.transform.Rotate(0, 0, 180);
    }

    #endregion

    #region Collision Methods

    //private void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.gameObject.tag.Contains("Racetrack") || coll.gameObject.tag.Contains("Powerup"))
    //    {
    //        Destroy(this.gameObject);
    //    }

    //    if (coll.gameObject.tag.Contains("Player") && (coll.gameObject.tag != carToFollow.tag))
    //    {
    //        coll.gameObject.GetComponent<Car_Controller>().health -= 20;
    //        coll.gameObject.GetComponent<Car_Controller>().collisionTimer = 60;
    //        Destroy(this.gameObject);

    //        if (coll.gameObject.GetComponent<Car_Controller>().health <= 0)
    //        {
    //            coll.gameObject.GetComponent<Car_Controller>().LostGame();
    //        }
    //    }
    //}

    #endregion
}
