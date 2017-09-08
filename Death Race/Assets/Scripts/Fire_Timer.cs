using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Timer : MonoBehaviour
{
    public int fireTimer = 240;
    
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        fireTimer--;

        if(fireTimer == 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Contains("Player"))
        {
            coll.gameObject.GetComponent<Rigidbody2D>().velocity = coll.gameObject.GetComponent<Rigidbody2D>().velocity * .85f;
            Destroy(this.gameObject);
        }

        if(coll.gameObject.tag == "Shield")
        {
            Destroy(this.gameObject);
        }
    }
}
