using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartEnd : NetworkBehaviour
{
    [SyncVar]
    public bool startGame = false;
    [SyncVar]
    public bool restartGame = false;
    [SyncVar]
    public bool endGame = false;
    [SyncVar]
    public float gameTime = 0;
    [SyncVar]
    public int playerNumber = 1;
    [SyncVar]
    public int playersFinished = 0;
    [SyncVar]
    public int numTopScores = 0;
    [SyncVar]
    public int numTopTimes = 0;
    [SyncVar]
    public string score1stPlace = "First 0000";
    [SyncVar]
    public string score2ndPlace = "Second 0000";
    [SyncVar]
    public string score3rdPlace = "Third 0000";
    [SyncVar]
    public string score4thPlace = "Fourth 0000";
    [SyncVar]
    public string score5thPlace = "Fifth 0000";
    [SyncVar]
    public string time1stPlace = "First 0000";
    [SyncVar]
    public string time2ndPlace = "Second 0000";
    [SyncVar]
    public string time3rdPlace = "Third 0000";
    [SyncVar]
    public string time4thPlace = "Fourth 0000";
    [SyncVar]
    public string time5thPlace = "Fifth 0000";

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        gameTime += Time.deltaTime;
	}
}
