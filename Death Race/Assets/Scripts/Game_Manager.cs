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
    public GameObject[] playerWin;
    public GameObject[] playerLose;
    public GameObject[] GameOverButtons;
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

        playerWin = GameObject.FindGameObjectsWithTag("P1 Win");
        playerLose = GameObject.FindGameObjectsWithTag("P1 Lose");
        GameOverButtons = GameObject.FindGameObjectsWithTag("GameOverButtons");
        texts = GameObject.FindGameObjectWithTag("Text");
        playerHealth = GameObject.FindGameObjectsWithTag("P1Health");

        #endregion

        #region Disable Canvas Elements

        foreach (GameObject obj in playerLose)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in playerWin)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in GameOverButtons)
        {
            obj.SetActive(false);
        }

        texts.SetActive(false);

        #endregion
    }

    public void GameOver(GameObject thisCar, bool isWinner, GameObject UICanvas)
    {
        #region Show Winner

        player.GetComponent<Car_Controller>().canMove = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        UICanvas.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 90f);

        foreach (GameObject obj in GameOverButtons)
        {
            obj.SetActive(true);
        }

        #endregion

        if (isWinner)
        {
            #region thisCar Wins

            playerWin[0].SetActive(true);
            playerWin[1].SetActive(true);

            #endregion
        }
        else
        {
            #region thisCar Loses

            playerLose[0].SetActive(true);
            playerLose[1].SetActive(true);

            #endregion
        }
    }

    public void RestartUI()
    {
        foreach (GameObject obj in playerLose)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in playerWin)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in GameOverButtons)
        {
            obj.SetActive(false);
        }
    }

    #endregion
}
