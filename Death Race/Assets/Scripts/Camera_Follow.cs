using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    float cameraZDistance = -10f;
    int numPlayers = 2;
    public GameObject followTarget1;
    public GameObject followTarget2;
    GameObject average;

	// Use this for initialization
	void Start ()
    {
        average = new GameObject();
        //followTarget1 = GameObject.FindGameObjectWithTag("Player1");
        //followTarget2 = GameObject.FindGameObjectWithTag("Player2");
    }
	
	// Update is called once per frame
	void Update ()
    {
        average.transform.position = new Vector3((followTarget1.transform.position.x + followTarget2.transform.position.x), (followTarget1.transform.position.y + followTarget2.transform.position.y), cameraZDistance);
        transform.position = new Vector3(followTarget1.transform.position.x, followTarget1.transform.position.y, cameraZDistance);
	}
}
