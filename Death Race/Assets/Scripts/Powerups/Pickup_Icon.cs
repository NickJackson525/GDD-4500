using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Icon : MonoBehaviour
{
    public GameObject followTarget;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(followTarget.transform.position.x - 5.1f, followTarget.transform.position.y + 8.3f, -1f);

        if(!followTarget.GetComponent<Car_Controller>().hasPickup)
        {
            Destroy(this.gameObject);
        }
    }
}
