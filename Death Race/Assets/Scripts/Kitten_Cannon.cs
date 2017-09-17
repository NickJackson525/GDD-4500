using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitten_Cannon : MonoBehaviour
{
    public GameObject kittenFollow;
    GameObject temp;
    public Vector3 directionToFire;
    public Rigidbody2D rb;
    public float speed = 12f;
    int timer = 100;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer--;

        rb.AddForce(-transform.up * speed);

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag.Contains("Player1"))
        {
            temp = Instantiate(kittenFollow);
            temp.GetComponent<Kitten_Follow>().followTarget = coll.gameObject;
            temp.gameObject.layer = 8;
            Destroy(this.gameObject);
        }

        if (coll.gameObject.tag.Contains("Player2"))
        {
            temp = Instantiate(kittenFollow);
            temp.GetComponent<Kitten_Follow>().followTarget = coll.gameObject;
            temp.gameObject.layer = 9;
            Destroy(this.gameObject);
        }
    }
}
