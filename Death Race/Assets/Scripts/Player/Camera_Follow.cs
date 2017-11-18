using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    float cameraZDistance = -10f;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Game_Manager.Instance.player)
        { 
            transform.position = new Vector3(Game_Manager.Instance.player.transform.position.x, Game_Manager.Instance.player.transform.position.y, cameraZDistance);
        }
	}
}
