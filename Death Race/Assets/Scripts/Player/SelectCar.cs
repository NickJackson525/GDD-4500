using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCar : MonoBehaviour
{
    public Game_Manager.CarType thisType = Game_Manager.CarType.NORMAL;
    GameObject player;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PickCar()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Game_Manager.Instance.currentCarType = thisType;

        player.GetComponent<Car_Controller>().health = Game_Manager.Instance.Cars[thisType][Game_Manager.CarTrait.HEALTH];
        player.GetComponent<Car_Controller>().speed = Game_Manager.Instance.Cars[thisType][Game_Manager.CarTrait.SPEED];
        player.GetComponent<Car_Controller>().turnPower = Game_Manager.Instance.Cars[thisType][Game_Manager.CarTrait.TURNPOWER];
    }
}
