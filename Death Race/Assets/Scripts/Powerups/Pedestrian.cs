using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pedestrian : NetworkBehaviour
{
    #region Variables

    GameObject createdInstance;
    GameObject collidedPlayer;
    public GameObject kittenIcon;
    public GameObject shieldIcon;
    public GameObject harpoonIcon;
    public GameObject fakePedestrianIcon;
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

    //[Command]
    void CmdSpawnPickup()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                collidedPlayer.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                createdInstance = Instantiate(fakePedestrianIcon, transform.position, transform.rotation);
                break;
            case 1:
                collidedPlayer.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.HARPOON;
                createdInstance = Instantiate(harpoonIcon, transform.position, transform.rotation);
                break;
            case 2:
                collidedPlayer.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
                createdInstance = Instantiate(kittenIcon, transform.position, transform.rotation);
                break;
            case 3:
                collidedPlayer.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.SHIELD;
                createdInstance = Instantiate(shieldIcon, transform.position, transform.rotation);
                break;
            default:
                collidedPlayer.GetComponent<Car_Controller>().currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                createdInstance = Instantiate(fakePedestrianIcon, transform.position, transform.rotation);
                break;
        }

        createdInstance.GetComponent<Pickup_Icon>().followTarget = collidedPlayer.gameObject;
        collidedPlayer.GetComponent<Car_Controller>().hasPickup = true;

        if (collidedPlayer.tag == "Player1")
        {
            createdInstance.GetComponent<Pickup_Icon>().gameObject.layer = 8;
        }
        else
        {
            createdInstance.GetComponent<Pickup_Icon>().gameObject.layer = 9;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if((coll.gameObject.tag.Contains("Player")) && coll.gameObject.GetComponent<Car_Controller>().localPlayerAuthority)
        {
            if (!coll.gameObject.GetComponent<Car_Controller>().hasPickup)
            {
                //only 50% chance to give pickup
                if (Random.Range(pickupChance, pickupChance) == pickupChance)
                {
                    collidedPlayer = coll.gameObject;
                    CmdSpawnPickup();
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
