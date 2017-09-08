using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Animation_Transition : MonoBehaviour
{
    public GameObject Shield_Close;
    public GameObject carToFollow;
    GameObject createdShield;
    int timer = 300;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer--;

        if (timer == 0)
        {
            createdShield = Instantiate(Shield_Close, transform.position, transform.rotation);
            createdShield.GetComponent<Shield_Animation_Close>().carToFollow = this.carToFollow;
            Destroy(this.gameObject);
        }

        this.transform.position = new Vector3(carToFollow.transform.position.x, carToFollow.transform.position.y, -1);
        this.transform.rotation = carToFollow.transform.rotation;
        this.transform.Rotate(0, 0, 180);
    }
}
