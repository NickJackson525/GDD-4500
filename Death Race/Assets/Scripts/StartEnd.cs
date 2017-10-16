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
    public bool endGame = false;
    [SyncVar]
    public float gameTime = 0;
    [SyncVar]
    public int playerNumber = 1;
    [SyncVar]
    public int playersFinished = 0;
    //[SyncVar]
    //public Text score1stPlace;
    //[SyncVar]
    //public Text score2ndPlace;
    //[SyncVar]
    //public Text score3rdPlace;
    //[SyncVar]
    //public Text time1stPlace;
    //[SyncVar]
    //public Text time2ndPlace;
    //[SyncVar]
    //public Text time3rdPlace;

    public SyncListString topScores = new SyncListString();
    public SyncListString topTimes = new SyncListString();

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        gameTime += Time.deltaTime;
	}

    [Command]
    public void CmdStartGame()
    {
        startGame = true;
    }
}
