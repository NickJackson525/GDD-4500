﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject highScoreTable;

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
        if(highScoreTable.activeSelf)
        {
            highScoreTable.SetActive(true);
        }
        else
        {
            highScoreTable.SetActive(false);
        }
    }
}
