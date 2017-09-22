using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finishline : MonoBehaviour
{
    GameObject[] allCheckpoints;
    public GameObject player1;
    public GameObject player2;
    public GameObject p1Canvas;
    public GameObject p2Canvas;

    // Use this for initialization
    void Start ()
    {
        Game_Manager.Instance.GameStart();
        allCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
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
                Game_Manager.Instance.GameOver(coll.gameObject, true, p1Canvas, p2Canvas);
            }
        }
    }
}
