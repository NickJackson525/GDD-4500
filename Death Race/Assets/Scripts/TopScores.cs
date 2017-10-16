using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopScores : MonoBehaviour
{
    public int place;
    public Text thisText;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Game_Manager.Instance.startEnd.topScores.Count <= place)
        {
            thisText.text = Game_Manager.Instance.startEnd.topScores[place - 1];
        }
	}
}
