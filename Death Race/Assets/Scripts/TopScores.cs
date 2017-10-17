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
        if (Game_Manager.Instance.startEnd)
        {
            switch (place)
            {
                case 1:
                    thisText.text = Game_Manager.Instance.startEnd.score1stPlace;
                    break;
                case 2:
                    thisText.text = Game_Manager.Instance.startEnd.score2ndPlace;
                    break;
                case 3:
                    thisText.text = Game_Manager.Instance.startEnd.score3rdPlace;
                    break;
                case 4:
                    thisText.text = Game_Manager.Instance.startEnd.score4thPlace;
                    break;
                case 5:
                    thisText.text = Game_Manager.Instance.startEnd.score5thPlace;
                    break;
                default:
                    thisText.text = Game_Manager.Instance.startEnd.score1stPlace;
                    break;
            }
        }
	}
}
