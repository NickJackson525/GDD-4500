using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Pickup : MonoBehaviour
{
    public GameObject Shield;
    GameObject createdShield;

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
        if (coll.gameObject.tag.Contains("Player") && !coll.GetComponent<Car_Controller>().hasShield)
        {
            createdShield = Instantiate(Shield, new Vector3(coll.transform.position.x, coll.transform.position.y - 1.63f, coll.transform.position.z), coll.transform.rotation, coll.transform);
            createdShield.GetComponent<Shield_Animation_Create>().carToFollow = coll.gameObject;
            coll.gameObject.GetComponent<Car_Controller>().hasShield = true;
            Destroy(this.gameObject);
        }
    }
}
