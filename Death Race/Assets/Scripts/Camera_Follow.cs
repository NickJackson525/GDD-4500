using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    float cameraZDistance = -10f;
    public GameObject followTarget;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, cameraZDistance);
	}
}
