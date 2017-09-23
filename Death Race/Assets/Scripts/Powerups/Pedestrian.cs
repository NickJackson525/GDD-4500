using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    #region Variables

    public GameObject kittenIcon;
    public GameObject shieldIcon;
    public GameObject harpoonIcon;
    public GameObject fakePedestrianIcon;
    GameObject createdInstance;
    int pickupChance = 2;

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
        if((coll.gameObject.tag.Contains("Player")))
        {
            if (!coll.gameObject.GetComponent<Car_Controller>().hasPickup)
            {
                //only 50% chance to give pickup
                if (Random.Range(pickupChance, pickupChance) == pickupChance)
                {
                    switch (Random.Range(0, 4))
                    {
                        case 0:
                            coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                            createdInstance = Instantiate(fakePedestrianIcon, transform.position, transform.rotation);
                            break;
                        case 1:
                            coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.HARPOON;
                            createdInstance = Instantiate(harpoonIcon, transform.position, transform.rotation);
                            break;
                        case 2:
                            coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
                            createdInstance = Instantiate(kittenIcon, transform.position, transform.rotation);
                            break;
                        case 3:
                            coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.SHIELD;
                            createdInstance = Instantiate(shieldIcon, transform.position, transform.rotation);
                            break;
                        default:
                            coll.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                            createdInstance = Instantiate(fakePedestrianIcon, transform.position, transform.rotation);
                            break;
                    }

                    createdInstance.GetComponent<Pickup_Icon>().followTarget = coll.gameObject;
                    coll.GetComponent<Car_Controller>().hasPickup = true;

                    if (coll.tag == "Player1")
                    {
                        createdInstance.GetComponent<Pickup_Icon>().gameObject.layer = 8;
                    }
                    else
                    {
                        createdInstance.GetComponent<Pickup_Icon>().gameObject.layer = 9;
                    }
                }

                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
