using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
    public int Score1 = 0;
    public int Score2 = 0;
    public int Score3 = 0;
    public int Score4 = 0;
    public int Score5 = 0;
    public bool creatingAccount = false;

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
        //UICanvas.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 90f);

        foreach (GameObject obj in GameOverButtons)
        {
            obj.SetActive(true);
            obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
        }

        #endregion

        if (isWinner)
        {
            #region thisCar Wins

            playerWin[0].SetActive(true);
            playerWin[0].transform.position = new Vector3(playerWin[0].transform.position.x, playerWin[0].transform.position.y, 0);
            playerWin[1].SetActive(true);
            playerWin[1].transform.position = new Vector3(playerWin[1].transform.position.x, playerWin[1].transform.position.y, 0);

            #endregion
        }
        else
        {
            #region thisCar Loses

            playerLose[0].SetActive(true);
            playerLose[0].transform.position = new Vector3(playerLose[0].transform.position.x, playerLose[0].transform.position.y, 0);
            playerLose[1].SetActive(true);
            playerLose[1].transform.position = new Vector3(playerLose[1].transform.position.x, playerLose[1].transform.position.y, 0);

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

    public void SaveFile(GameObject UIController, string username, string password, int score1 = 0, int score2 = 0, int score3 = 0, int score4 = 0, int score5 = 0)
    {
        string destination = Application.persistentDataPath + "/" + username + ".dat";
        FileStream file;

        //TODO: check if username and password are correct if the file exists (file exists means username already exists)

        if (File.Exists(destination))
        {
            file = File.OpenWrite(destination);
            SaveData data = new SaveData(password, score1, score2, score3, score4, score5);

            if ((password == data.getPassword()) && !creatingAccount)
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, data);
                file.Close();
            }
            else if(creatingAccount)
            {
                //username already exists
                UIController.GetComponent<UIController>().ErrorPopup(2);
            }
            else if(password != data.getPassword())
            {
                //incorrect password
                UIController.GetComponent<UIController>().ErrorPopup(1);
            }
        }
        else
        {
            creatingAccount = false;
            file = File.Create(destination);
            SaveData data = new SaveData(password, score1, score2, score3, score4, score5);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();

            UIController.GetComponent<UIController>().SuccessCreateAccount();
        }
    }

    public void LoadFile(GameObject Controller, string username, string password)
    {
        string destination = Application.persistentDataPath + "/" + username + ".dat";
        FileStream file;

        //check if username already exists
        if (File.Exists(destination))
        {
            file = File.OpenRead(destination);
        }
        else
        {
            Controller.GetComponent<UIController>().ErrorPopup(3);
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        SaveData data = (SaveData)bf.Deserialize(file);
        file.Close();

        if(password == data.getPassword())
        {
            //correct password
            Score1 = data.Score1;
            Score2 = data.Score2;
            Score3 = data.Score3;
            Score4 = data.Score4;
            Score5 = data.Score5;

            Controller.GetComponent<UIController>().SuccessfulLogin();
        }
        else
        {
            //incorrect password
            Controller.GetComponent<UIController>().ErrorPopup(1);
        }
    }

    #endregion
}
