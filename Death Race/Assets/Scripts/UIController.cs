using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject highScoreTable;
    public GameObject bestTimesTable;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

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
}
