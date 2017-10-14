using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StartEnd : NetworkBehaviour
{
    [SyncVar]
    public bool startGame = false;
    [SyncVar]
    public bool endGame = false;
    [SyncVar]
    public float gameTime = 0;
    [SyncVar]
    public int playersFinished = 0;

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
