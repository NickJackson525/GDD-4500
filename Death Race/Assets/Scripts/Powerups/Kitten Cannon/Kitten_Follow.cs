﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Kitten_Follow : NetworkBehaviour
{
    [SyncVar]
    public GameObject followTarget;

    enum SwerveDirection { LEFT, RIGHT }
    int sequenceCount = 0;
    int sequenceGoal = 4;
    SwerveDirection nextDirection = SwerveDirection.LEFT;
    public Vector3 targetPosition;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, -1f);

        if ((Input.GetKeyDown (KeyCode.A)) && (nextDirection == SwerveDirection.LEFT))
        {
            sequenceCount++;
            nextDirection = SwerveDirection.RIGHT;
            //targetPosition = new Vector3(followTarget.transform.position.x - 3f, followTarget.transform.position.y, followTarget.transform.position.z);
        }
        if ((Input.GetKeyDown(KeyCode.D)) && (nextDirection == SwerveDirection.RIGHT))
        {
            sequenceCount++;
            nextDirection = SwerveDirection.LEFT;
            //targetPosition = new Vector3(followTarget.transform.position.x + 3f, followTarget.transform.position.y, followTarget.transform.position.z);
        }

        transform.position = targetPosition;

        if(sequenceCount >= sequenceGoal)
        {
            Destroy(this.gameObject);
        }
	}
}
