using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool playerOnePassed = false;
    bool playerTwoPassed = false;

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
        if (coll.gameObject.tag.Contains("Player"))
        {
            if ((coll.gameObject.tag == "Player1") && (!playerOnePassed))
            {
                coll.gameObject.GetComponent<Car_Controller>().checkpointsPassed++;
                playerOnePassed = true;
            }

            if ((coll.gameObject.tag == "Player2") && (!playerTwoPassed))
            {
                coll.gameObject.GetComponent<Car_Controller>().checkpointsPassed++;
                playerTwoPassed = true;
            }
        }
    }
}
