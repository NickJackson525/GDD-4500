using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Start_Countdown : NetworkBehaviour
{
    public Sprite noLightsOn;
    public Sprite oneRedLightOn;
    public Sprite twoRedLightsOn;
    public Sprite threeRedLightsOn;
    public Sprite allGreenLightsOn;
    public int timer = 60;
    public GameObject WASD;
    public GameObject Arrows;
    public GameObject Enter_Space;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Game_Manager.Instance.startEnd && Game_Manager.Instance.startEnd.startGame)
        {
            timer--;

            if (timer == 0)
            {
                switch (this.gameObject.GetComponent<SpriteRenderer>().sprite.name)
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
                        WASD.SetActive(false);
                        Enter_Space.SetActive(false);
                        gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
	}

    private void OnEnable()
    {
        WASD.SetActive(true);
        Enter_Space.SetActive(true);
        timer = 60;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = noLightsOn;
    }
}
