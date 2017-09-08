using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finishline : MonoBehaviour
{
    public GameObject[] p1Win;
    public GameObject[] p1Lose;
    public GameObject[] p2Win;
    public GameObject[] p2Lose;
    public GameObject p1Canvas;
    public GameObject p2Canvas;
    public GameObject player1;
    public GameObject player2;
    GameObject[] allCheckpoints;

	// Use this for initialization
	void Start ()
    {
        allCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        p1Win = GameObject.FindGameObjectsWithTag("P1 Win");
        p1Lose = GameObject.FindGameObjectsWithTag("P1 Lose");
        p2Win = GameObject.FindGameObjectsWithTag("P2 Win");
        p2Lose = GameObject.FindGameObjectsWithTag("P2 Lose");
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        p1Win[0].SetActive(false);
        p1Win[1].SetActive(false);
        p1Lose[0].SetActive(false);
        p1Lose[1].SetActive(false);
        p2Lose[0].SetActive(false);
        p2Lose[1].SetActive(false);
        p2Win[0].SetActive(false);
        p2Win[1].SetActive(false);
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
                player1.GetComponent<Car_Controller>().canMove = false;
                player1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                player2.GetComponent<Car_Controller>().canMove = false;
                player2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                p1Canvas.SetActive(true);
                p1Canvas.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 90);
                p2Canvas.SetActive(true);
                p2Canvas.transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 90);

                if (coll.gameObject.tag == "Player1")
                {
                    p1Lose[0].SetActive(true);
                    //p1Lose[0].transform.position = new Vector3(p1Lose[0].transform.position.x, p1Lose[0].transform.position.y, -3539.054f);
                    p1Lose[1].SetActive(true);
                    //p1Lose[1].transform.position = new Vector3(p1Lose[1].transform.position.x, p1Lose[1].transform.position.y, -3539.054f);
                    p2Win[0].SetActive(true);
                    //p2Win[0].transform.position = new Vector3(p2Win[0].transform.position.x, p2Win[0].transform.position.y, -3539.054f);
                    p2Win[1].SetActive(true);
                    //p2Win[1].transform.position = new Vector3(p2Win[1].transform.position.x, p2Win[1].transform.position.y, -3539.054f);
                }
                else if (coll.gameObject.tag == "Player2")
                {
                    p1Win[0].SetActive(true);
                    //p1Win[0].transform.position = new Vector3(p1Win[0].transform.position.x, p1Win[0].transform.position.y, -3539.054f);
                    p1Win[1].SetActive(true);
                    //p1Win[1].transform.position = new Vector3(p1Win[1].transform.position.x, p1Win[1].transform.position.y, -3539.054f);
                    p2Lose[0].SetActive(true);
                    //p2Lose[0].transform.position = new Vector3(p2Lose[0].transform.position.x, p2Lose[0].transform.position.y, -3539.054f);
                    p2Lose[1].SetActive(true);
                    //p2Lose[1].transform.position = new Vector3(p2Lose[1].transform.position.x, p2Lose[1].transform.position.y, -3539.054f);
                }
            }
        }
    }
}
