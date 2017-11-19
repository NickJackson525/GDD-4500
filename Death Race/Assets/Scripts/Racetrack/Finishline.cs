using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Finishline : NetworkBehaviour
{
    GameObject createdPlayer;
    GameObject spawnPoint;
    GameObject[] allCheckpoints;
    public GameObject UICanvas;
    public GameObject playerPrefab;

    // Use this for initialization
    void Start ()
    {
        Game_Manager.Instance.GameStart();
        //CmdSpawnPlayer();
        allCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    //[Command]
    //private void CmdSpawnPlayer()
    //{
    //    createdPlayer = Instantiate(playerPrefab, transform.position, transform.rotation);
    //    NetworkServer.Spawn(createdPlayer);

    //    switch (createdPlayer.GetComponent<Car_Controller>().playerNumber)
    //    {
    //        case 1:
    //            spawnPoint = GameObject.Find("SpawnPosition1");
    //            break;
    //        case 2:
    //            spawnPoint = GameObject.Find("SpawnPosition2");
    //            break;
    //        case 3:
    //            spawnPoint = GameObject.Find("SpawnPosition3");
    //            break;
    //        case 4:
    //            spawnPoint = GameObject.Find("SpawnPosition4");
    //            break;
    //        case 5:
    //            spawnPoint = GameObject.Find("SpawnPosition5");
    //            break;
    //        case 6:
    //            spawnPoint = GameObject.Find("SpawnPosition6");
    //            break;
    //        case 7:
    //            spawnPoint = GameObject.Find("SpawnPosition7");
    //            break;
    //        case 8:
    //            spawnPoint = GameObject.Find("SpawnPosition8");
    //            break;
    //    }

    //    createdPlayer.transform.position = spawnPoint.transform.position;
    //}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Contains("Player"))
        {
            if (coll.gameObject.GetComponent<Car_Controller>().checkpointsPassed >= allCheckpoints.Length)
            {
                coll.gameObject.GetComponent<Car_Controller>().CmdGameOver(true);
            }
        }
    }
}
