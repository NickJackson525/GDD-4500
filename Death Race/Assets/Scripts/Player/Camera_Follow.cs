using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    float cameraZDistance = -10f;
    public GameObject followTarget;
    public int playerNumToFollow = 0;

	// Use this for initialization
	void Start ()
    {
        if(playerNumToFollow == 0)
        {
            followTarget = GameObject.FindGameObjectWithTag("Player1");
        }
        else
        {
            followTarget = GameObject.FindGameObjectWithTag("Player2");
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerNumToFollow == 0)
        {
            followTarget = GameObject.FindGameObjectWithTag("Player1");
        }
        else
        {
            followTarget = GameObject.FindGameObjectWithTag("Player2");
        }

        if (followTarget != null)
        {
            transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, cameraZDistance);
        }
	}
}
