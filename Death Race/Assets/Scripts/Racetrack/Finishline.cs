using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finishline : MonoBehaviour
{
    GameObject[] allCheckpoints;
    public GameObject UICanvas;

    // Use this for initialization
    void Start ()
    {
        Game_Manager.Instance.GameStart();
        allCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Contains("Player"))
        {
            if (coll.gameObject.GetComponent<Car_Controller>().checkpointsPassed == allCheckpoints.Length)
            {
                coll.gameObject.GetComponent<Car_Controller>().CmdGameOver(true);
            }
        }
    }
}
