using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitten_Follow : MonoBehaviour
{
    public GameObject followTarget;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, -1f);
	}
}
