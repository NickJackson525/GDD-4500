using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject mainMenuWindow;
    public GameObject loginCreateAccountWindow;
    public GameObject lobbyWindow;
    public GameObject highScoreTable;
    public GameObject bestTimesTable;
    public InputField Username;
    public InputField Password;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void Login()
    {
        mainMenuWindow.SetActive(false);
        loginCreateAccountWindow.SetActive(true);
        lobbyWindow.SetActive(false);
        Game_Manager.Instance.creatingAccount = false;
    }

    public void CreateAccount()
    {
        mainMenuWindow.SetActive(false);
        loginCreateAccountWindow.SetActive(true);
        lobbyWindow.SetActive(false);
        Game_Manager.Instance.creatingAccount = true;
    }

    public void Submit()
    {
        if(Game_Manager.Instance.creatingAccount)
        {
            Game_Manager.Instance.SaveFile(gameObject, Username.GetComponentInChildren<Text>().text, Password.GetComponentInChildren<Text>().text);
            mainMenuWindow.SetActive(true);
            loginCreateAccountWindow.SetActive(false);
            lobbyWindow.SetActive(false);
        }
        else
        {
            Game_Manager.Instance.LoadFile(gameObject, Username.GetComponentInChildren<Text>().text, Password.GetComponentInChildren<Text>().text);
            mainMenuWindow.SetActive(false);
            loginCreateAccountWindow.SetActive(false);
            lobbyWindow.SetActive(true);
        }
    }

    public void BackToMainMenu()
    {
        mainMenuWindow.SetActive(true);
        loginCreateAccountWindow.SetActive(false);
        lobbyWindow.SetActive(false);
        Game_Manager.Instance.creatingAccount = false;
    }

    public void ShowHideHighScoreTable()
    {
        if(!highScoreTable.activeSelf)
        {
            highScoreTable.SetActive(true);
        }
        else
        {
            highScoreTable.SetActive(false);
        }
    }

    public void ShowHideBestTimeTable()
    {
        if (!bestTimesTable.activeSelf)
        {
            bestTimesTable.SetActive(true);
        }
        else
        {
            bestTimesTable.SetActive(false);
        }
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Restart()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<Car_Controller>().CmdRestartGame();
    }
}
