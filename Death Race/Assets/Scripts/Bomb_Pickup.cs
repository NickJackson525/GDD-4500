using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Pickup : MonoBehaviour
{
    public GameObject Bomb_Follow;
    Quaternion collRotation;

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
        collRotation = coll.transform.rotation;

        if (coll.gameObject.tag.Contains("Player") && !coll.GetComponent<Car_Controller>().hasBomb)
        {
            coll.transform.rotation = this.transform.rotation;
            coll.gameObject.GetComponent<Car_Controller>().bombFollow = Instantiate(Bomb_Follow, new Vector3(coll.transform.position.x, coll.transform.position.y - 1.63f, coll.transform.position.z), coll.transform.rotation, coll.transform);
            coll.gameObject.GetComponent<Car_Controller>().hasBomb = true;
            coll.transform.rotation = collRotation;
            Destroy(this.gameObject);
        }
    }
}
