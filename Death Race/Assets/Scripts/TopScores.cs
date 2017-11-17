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
        //DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (place)
        {
            case 1:
                thisText.text = "1 - " + Game_Manager.Instance.Score1.ToString();
                break;
            case 2:
                thisText.text = "2 - " + Game_Manager.Instance.Score2.ToString();
                break;
            case 3:
                thisText.text = "3 - " + Game_Manager.Instance.Score3.ToString();
                break;
            case 4:
                thisText.text = "4 - " + Game_Manager.Instance.Score4.ToString();
                break;
            case 5:
                thisText.text = "5 - " + Game_Manager.Instance.Score5.ToString();
                break;
            default:
                thisText.text = "1 - " + Game_Manager.Instance.Score1.ToString();
                break;
        }
	}
}
