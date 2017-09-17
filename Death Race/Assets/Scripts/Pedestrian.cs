using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    #region Variables

    int pickupChance = 0;

    #endregion

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag.Contains("Player"))
        {
            //only 50% chance to give pickup
            if(Random.Range(0, 1) == 0)
            {
                switch(Random.Range(0, 4))
                {
                    case 0:
                        coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                        break;
                    case 1:
                        coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.HARPOON;
                        break;
                    case 2:
                        coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
                        break;
                    case 3:
                        coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.SHIELD;
                        break;
                    default:
                        coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                        break;
                }

                coll.GetComponent<Car_Controller>().hasPickup = true;
            }

            Destroy(this.gameObject);
        }
    }
}
