﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    bool playerPassed = false;

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
            coll.gameObject.GetComponent<Car_Controller>().checkpointsPassed++;
            playerPassed = true;
        }
    }
}
