using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Fake_Pedestrian : NetworkBehaviour
{
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag.Contains("Player"))
        {
            coll.gameObject.GetComponent<Car_Controller>().health -= 40;
            Destroy(this.gameObject);
        }
    }
}
