using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class Game_Manager
{
    #region Variables

    public GameObject player;
    public enum Pickup { FAKE_PEDESTRIAN, KITTEN_CANNON, HARPOON, SHIELD }
    //public GameObject[] p2Win;
    //public GameObject[] p2Lose;
    public GameObject[] playerWin;
    public GameObject[] playerLose;
    public GameObject[] playerHealth;
    public GameObject texts;
    public StartEnd startEnd;

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

        //p2Win = GameObject.FindGameObjectsWithTag("P1 Win");
        //p2Lose = GameObject.FindGameObjectsWithTag("P1 Lose");
        playerWin = GameObject.FindGameObjectsWithTag("P1 Win");
        playerLose = GameObject.FindGameObjectsWithTag("P1 Lose");
        texts = GameObject.FindGameObjectWithTag("Text");
        playerHealth = GameObject.FindGameObjectsWithTag("P1Health");
        //p2Health = GameObject.FindGameObjectsWithTag("P2Health");

        #endregion

        #region Disable Canvas Elements

        //p2Win[0].SetActive(false);
        //p2Win[1].SetActive(false);
        //p2Lose[0].SetActive(false);
        //p2Lose[1].SetActive(false);
        playerLose[0].SetActive(false);
        playerLose[1].SetActive(false);
        playerWin[0].SetActive(false);
        playerWin[1].SetActive(false);
        texts.SetActive(false);

        #endregion
    }

    public void GameOver(GameObject thisCar, bool isWinner, GameObject UICanvas)
    {
        #region Show Winner

        player.GetComponent<Car_Controller>().canMove = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        UICanvas.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 90f);

        #endregion

        if (isWinner)
        {
            #region thisCar Wins

            //if (thisCar.gameObject.tag == "Player1")
            //{
                //p2Lose[0].SetActive(true);
                //p2Lose[0].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                //p2Lose[1].SetActive(true);
                //p2Lose[1].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                playerWin[0].SetActive(true);
                playerWin[0].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
                playerWin[1].SetActive(true);
                playerWin[1].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
            //}
            //else if (thisCar.gameObject.tag == "Player2")
            //{
            //    //p2Win[0].SetActive(true);
            //    //p2Win[0].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
            //    //p2Win[1].SetActive(true);
            //    //p2Win[1].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
            //    p1Lose[0].SetActive(true);
            //    p1Lose[0].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
            //    p1Lose[1].SetActive(true);
            //    p1Lose[1].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
            //}

            #endregion
        }
        else
        {
            #region thisCar Loses

            //if (thisCar.gameObject.tag == "Player2")
            //{
            //    p2Lose[0].SetActive(true);
            //    p2Lose[0].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
            //    p2Lose[1].SetActive(true);
            //    p2Lose[1].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                //playerWin[0].SetActive(true);
                //playerWin[0].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
                //playerWin[1].SetActive(true);
                //playerWin[1].transform.position = new Vector3(player1.transform.position.x, player1.transform.position.y, 0f);
            //}
            //else if (thisCar.gameObject.tag == "Player1")
            //{
            //    p2Win[0].SetActive(true);
            //    p2Win[0].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
            //    p2Win[1].SetActive(true);
            //    p2Win[1].transform.position = new Vector3(player2.transform.position.x, player2.transform.position.y, 0f);
                playerLose[0].SetActive(true);
                playerLose[0].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
                playerLose[1].SetActive(true);
                playerLose[1].transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
            //}

            #endregion
        }
    }

    //public void AddCar(GameObject carToAdd)
    //{
    //    Cars.Add(carToAdd);
    //    playerNumber++;
    //}

    #endregion
}
