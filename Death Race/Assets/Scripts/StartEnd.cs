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

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    [Command]
    public void CmdStartGame()
    {
        startGame = true;
    }
}
