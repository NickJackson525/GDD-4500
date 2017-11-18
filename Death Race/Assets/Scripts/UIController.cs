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
    public GameObject errorPopup;
    public GameObject successPopup;
    public GameObject ErrorMessage1;
    public GameObject ErrorMessage2;
    public GameObject ErrorMessage3;
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
            Game_Manager.Instance.SaveFile(gameObject, Username.text, Password.text);
        }
        else
        {
            Game_Manager.Instance.LoadFile(gameObject, Username.text, Password.text);
        }
    }

    public void SuccessfulLogin()
    {
        mainMenuWindow.SetActive(false);
        loginCreateAccountWindow.SetActive(false);
        lobbyWindow.SetActive(true);
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

    public void ErrorPopup(int error)
    {
        if (!errorPopup.activeSelf)
        {
            switch (error)
            {
                case 1:
                    ErrorMessage1.SetActive(true);
                    break;
                case 2:
                    ErrorMessage2.SetActive(true);
                    break;
                case 3:
                    ErrorMessage3.SetActive(true);
                    break;
            }

            errorPopup.SetActive(true);
        }
        else
        {
            ErrorMessage1.SetActive(false);
            ErrorMessage2.SetActive(false);
            ErrorMessage3.SetActive(false);
            errorPopup.SetActive(false);
        }
    }

    public void SuccessCreateAccount()
    {
        if (!successPopup.activeSelf)
        {
            successPopup.SetActive(true);
        }
        else
        {
            successPopup.SetActive(false);
            BackToMainMenu();
        }
    }

    public void Logout()
    {
        Game_Manager.Instance.SaveFile(gameObject, Username.text, Password.text, Game_Manager.Instance.Score1, Game_Manager.Instance.Score2, Game_Manager.Instance.Score3, Game_Manager.Instance.Score4, Game_Manager.Instance.Score5);
        BackToMainMenu();
    }
}
