using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game_Manager
{
    #region Variables

    public enum Pickup { FAKE_PEDESTRIAN, KITTEN_CANNON, HARPOON, SHIELD }
    public GameObject[] p2Win;
    public GameObject[] p2Lose;
    public GameObject[] p1Win;
    public GameObject[] p1Lose;
    public GameObject player1;
    public GameObject player2;
    public GameObject[] texts;

    #endregion

    #region Singleton

    // create variable for storing singleton that any script can access
    private static Game_Manager instance;

    // create GameManager
    private Game_Manager()
    {

    }

    // Property for Singleton
    public static Game_Manager Instance
    {
        get
        {
            // If the singleton does not exist
            if (instance == null)
            {
                // create and return it
                instance = new Game_Manager();
            }

            // otherwise, just return it
            return instance;
        }
    }

    #endregion

    #region Custom Methods

    public void GameStart()
    {
        #region Get Object References

        p2Win = GameObject.FindGameObjectsWithTag("P1 Win");
        p2Lose = GameObject.FindGameObjectsWithTag("P1 Lose");
        p1Win = GameObject.FindGameObjectsWithTag("P2 Win");
        p1Lose = GameObject.FindGameObjectsWithTag("P2 Lose");
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        texts = GameObject.FindGameObjectsWithTag("Text");

        #endregion

        #region Disable Canvas Elements

        p2Win[0].SetActive(false);
        p2Win[1].SetActive(false);
        p2Lose[0].SetActive(false);
        p2Lose[1].SetActive(false);
        p1Lose[0].SetActive(false);
        p1Lose[1].SetActive(false);
        p1Win[0].SetActive(false);
        p1Win[1].SetActive(false);
        texts[0].SetActive(false);
        texts[1].SetActive(false);

        #endregion
    }

    public void GameOver(GameObject thisCar, bool isWinner, GameObject p1Canvas, GameObject p2Canvas)
    {
        #region Show Winner

        player1.GetComponent<Car_Controller>().canMove = false;
        player1.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player2.GetComponent<Car_Controller>().canMove = false;
        player2.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        p1Canvas.transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 90f);
        p2Canvas.transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 90f);

        #endregion

        if (isWinner)
        {
            #region thisCar Wins

            if (thisCar.gameObject.tag == "Player1")
            {
                p2Lose[0].SetActive(true);
                p2Lose[0].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                p2Lose[1].SetActive(true);
                p2Lose[1].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                p1Win[0].SetActive(true);
                p1Win[0].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
                p1Win[1].SetActive(true);
                p1Win[1].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
            }
            else if (thisCar.gameObject.tag == "Player2")
            {
                p2Win[0].SetActive(true);
                p2Win[0].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                p2Win[1].SetActive(true);
                p2Win[1].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                p1Lose[0].SetActive(true);
                p1Lose[0].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
                p1Lose[1].SetActive(true);
                p1Lose[1].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
            }

            #endregion
        }
        else
        {
            #region thisCar Loses

            if (thisCar.gameObject.tag == "Player2")
            {
                p2Lose[0].SetActive(true);
                p2Lose[0].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                p2Lose[1].SetActive(true);
                p2Lose[1].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                p1Win[0].SetActive(true);
                p1Win[0].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
                p1Win[1].SetActive(true);
                p1Win[1].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
            }
            else if (thisCar.gameObject.tag == "Player1")
            {
                p2Win[0].SetActive(true);
                p2Win[0].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                p2Win[1].SetActive(true);
                p2Win[1].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                p1Lose[0].SetActive(true);
                p1Lose[0].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
                p1Lose[1].SetActive(true);
                p1Lose[1].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
            }

            #endregion
        }
    }

    #endregion
}
