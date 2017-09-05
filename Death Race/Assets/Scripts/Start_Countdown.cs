﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start_Countdown : MonoBehaviour
{
    public Sprite oneRedLightOn;
    public Sprite twoRedLightsOn;
    public Sprite threeRedLightsOn;
    public Sprite allGreenLightsOn;
    public int timer = 60;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer--;

        if(timer == 0)
        {
            switch(this.gameObject.GetComponent<SpriteRenderer>().sprite.name)
            {
                case "All_Off":
                    timer = 60;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = oneRedLightOn;
                    break;
                case "1_Red_On":
                    timer = 60;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = twoRedLightsOn;
                    break;
                case "2_Red_On":
                    timer = 60;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = threeRedLightsOn;
                    break;
                case "3_Red_On":
                    timer = 60;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = allGreenLightsOn;
                    break;
                case "3_Green_On":
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
	}
}
