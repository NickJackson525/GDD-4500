using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Explosion : MonoBehaviour
{
    public GameObject fire;
    public int countdown = 60;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        countdown--;

        if(countdown == 0)
        {
            Instantiate(fire, transform.position, transform.rotation);
            Instantiate(fire, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), transform.rotation);
            Instantiate(fire, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), transform.rotation);
            Instantiate(fire, new Vector3(transform.position.x - .75f, transform.position.y + .75f, transform.position.z), transform.rotation);
            Instantiate(fire, new Vector3(transform.position.x - .75f, transform.position.y - .75f, transform.position.z), transform.rotation);
            Instantiate(fire, new Vector3(transform.position.x + .75f, transform.position.y - .75f, transform.position.z), transform.rotation);
            Instantiate(fire, new Vector3(transform.position.x + .75f, transform.position.y + .75f, transform.position.z), transform.rotation);
            Instantiate(fire, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            Instantiate(fire, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), transform.rotation);
            Destroy(this.gameObject);
        }
	}
}
