using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Car_Controller : NetworkBehaviour
{
    #region Variables

    [SyncVar]
    public int health = 100;
    [SyncVar]
    public int score = 0;

    public Game_Manager.Pickup currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
    public bool hasPickup = false;
    GameObject createdIcon;
    GameObject createdPickup;
    public GameObject kittenCannon;
    public GameObject kittenFollowPrefab;
    public GameObject shield;
    public GameObject fakePedestrian;
    public GameObject harpoon;
    public GameObject UICanvas;
    public GameObject kittenIcon;
    public GameObject shieldIcon;
    public GameObject harpoonIcon;
    public GameObject fakePedestrianIcon;
    public GameObject startEndController;
    GameObject startLight;
    GameObject temp;
    float speed = 17f;
    float turnPower = -110f;
    float driftPower = 0.95f;
    public int playerNumber = 0;
    public int checkpointsPassed = 0;
    public int collisionTimer = 0;
    public bool hasBomb = false;
    public bool hasShield = false;
    public bool canMove = true;
    bool canCollide = true;
    Quaternion initialRotation;
    Quaternion tempRotation;
    Rigidbody2D rb;
    Vector3 startPosition;
    Quaternion startRotation;
    string prevSceneName;

    #endregion

    #region Start

    //public override void OnStartServer()
    //{
    //    if (!Game_Manager.Instance.startEnd)
    //    {
    //        GameObject newObject = Instantiate(startEndController);
    //        NetworkServer.Spawn(newObject);
    //        Game_Manager.Instance.startEnd = newObject.GetComponent<StartEnd>();
    //    }
    //}

    //public override void OnStartLocalPlayer()
    //{
    //    Game_Manager.Instance.player = gameObject;
    //    startPosition = transform.position;
    //    startRotation = transform.rotation;

    //    if (!Game_Manager.Instance.startEnd)
    //    {
    //        Game_Manager.Instance.startEnd = GameObject.Find("StartEndController(Clone)").GetComponent<StartEnd>();
    //    }

    //    playerNumber = Game_Manager.Instance.startEnd.playerNumber;
    //    Game_Manager.Instance.startEnd.playerNumber++;
    //}

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        DontDestroyOnLoad(this);

        //if (!Game_Manager.Instance.startEnd)
        //{
        //    GameObject newObject = Instantiate(startEndController);
        //    NetworkServer.Spawn(newObject);
        //    Game_Manager.Instance.startEnd = newObject.GetComponent<StartEnd>();
        //}

        //Game_Manager.Instance.player = gameObject;
        //startPosition = transform.position;
        //startRotation = transform.rotation;

        //if (!Game_Manager.Instance.startEnd)
        //{
        //    Game_Manager.Instance.startEnd = GameObject.Find("StartEndController(Clone)").GetComponent<StartEnd>();
        //}

        //playerNumber = Game_Manager.Instance.startEnd.playerNumber;
        //Game_Manager.Instance.startEnd.playerNumber++;

        //startLight = GameObject.FindGameObjectWithTag("StartLight");
        //initialRotation = transform.rotation;
        //UICanvas = GameObject.FindGameObjectWithTag("p1Canvas");
        //rb = GetComponent<Rigidbody2D>();
    }

    #endregion

    #region Updates

    void Update()
    {
        if (isLocalPlayer)
        {
            if (prevSceneName != SceneManager.GetActiveScene().name)
            {
                if (SceneManager.GetActiveScene().name == "Map")
                {
                    NetworkServer.SpawnObjects();

                    if (!Game_Manager.Instance.startEnd)
                    {
                        GameObject newObject = Instantiate(startEndController);
                        NetworkServer.Spawn(newObject);
                        Game_Manager.Instance.startEnd = newObject.GetComponent<StartEnd>();
                    }

                    Game_Manager.Instance.player = gameObject;
                    startPosition = transform.position;
                    startRotation = transform.rotation;

                    if (!Game_Manager.Instance.startEnd)
                    {
                        Game_Manager.Instance.startEnd = GameObject.Find("StartEndController(Clone)").GetComponent<StartEnd>();
                    }

                    playerNumber = Game_Manager.Instance.startEnd.playerNumber;
                    Game_Manager.Instance.startEnd.playerNumber++;

                    startLight = GameObject.FindGameObjectWithTag("StartLight");
                    initialRotation = transform.rotation;
                    UICanvas = GameObject.FindGameObjectWithTag("p1Canvas");
                    rb = GetComponent<Rigidbody2D>();
                    prevSceneName = SceneManager.GetActiveScene().name;
                }
            }
            else
            {
                prevSceneName = SceneManager.GetActiveScene().name;
            }

            if ((playerNumber == 0) && (Game_Manager.Instance.startEnd))
            {
                playerNumber = Game_Manager.Instance.startEnd.playerNumber;
                Game_Manager.Instance.startEnd.playerNumber++;
            }

            if (Game_Manager.Instance.startEnd && startLight && !Game_Manager.Instance.startEnd.startGame && !startLight.activeSelf)
            {
                ResetGame();
            }

            if (Game_Manager.Instance.startEnd && Game_Manager.Instance.startEnd.restartGame)
            {
                CmdRestartGame();
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                CmdRestartGame();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                CmdStartGame();
            }

            if (!Game_Manager.Instance.startEnd && (SceneManager.GetActiveScene().name == "Map"))
            {
                Game_Manager.Instance.startEnd = GameObject.Find("StartEndController(Clone)").GetComponent<StartEnd>();
            }

            #region Player 1 Health

            if (Game_Manager.Instance.playerHealth != null)
            {
                Game_Manager.Instance.playerHealth[0].transform.position = transform.position;
                Game_Manager.Instance.playerHealth[0].transform.rotation = transform.rotation;
                Game_Manager.Instance.playerHealth[1].transform.position = transform.position;
                Game_Manager.Instance.playerHealth[1].transform.rotation = transform.rotation;

                if (Game_Manager.Instance.playerHealth[0].name == "HealthBar")
                {
                    Game_Manager.Instance.playerHealth[0].GetComponent<Image>().fillAmount = (float)health / 100f;

                    if (health <= 25)
                    {
                        Game_Manager.Instance.playerHealth[0].GetComponent<Image>().color = Color.red;
                    }
                    else if (health <= 50)
                    {
                        Game_Manager.Instance.playerHealth[0].GetComponent<Image>().color = Color.yellow;
                    }
                }
                else
                {
                    Game_Manager.Instance.playerHealth[1].GetComponent<Image>().fillAmount = (float)health / 100f;

                    if (health <= 25)
                    {
                        Game_Manager.Instance.playerHealth[1].GetComponent<Image>().color = Color.red;
                    }
                    else if (health <= 50)
                    {
                        Game_Manager.Instance.playerHealth[1].GetComponent<Image>().color = Color.yellow;
                    }
                }
            }
            #endregion
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            if (SceneManager.GetActiveScene().name == "Map")
            {
                rb.velocity = getForewordVelocity(rb) + getSidewaysVelocity(rb) * driftPower;

                if ((!startLight || (startLight.activeSelf == false)) && canMove)
                {
                    #region Player Controls

                    if (Input.GetKey(KeyCode.W))
                    {
                        rb.AddForce(transform.up * speed);
                    }

                    if (Input.GetKey(KeyCode.S))
                    {
                        rb.AddForce(transform.up * (-speed / 2));
                    }

                    if (Input.GetKey(KeyCode.A))
                    {
                        rb.angularVelocity = -turnPower;
                    }

                    if (Input.GetKey(KeyCode.D))
                    {
                        rb.angularVelocity = turnPower;
                    }

                    if (hasPickup && Input.GetKeyUp(KeyCode.Space))
                    {
                        CmdUsePickup(currentPickup);
                        hasPickup = false;
                    }

                    #endregion
                }
                else if ((startLight && (startLight.activeSelf == false)) && Game_Manager.Instance.startEnd.startGame)
                {
                    startLight.SetActive(false);
                }

                if (collisionTimer > 0)
                {
                    collisionTimer--;
                }
                else
                {
                    canCollide = true;
                }
            }
        }
    }

    #endregion

    #region Custom Methods

    Vector2 getForewordVelocity(Rigidbody2D rb)
    {
        return transform.up * Vector2.Dot(rb.velocity, transform.up);
    }

    Vector2 getSidewaysVelocity(Rigidbody2D rb)
    {
        return transform.right * Vector2.Dot(rb.velocity, transform.right);
    }

    //[Command]
    public void CmdGameOver(bool didWin)
    {
        Game_Manager.Instance.startEnd.playersFinished++;
        Game_Manager.Instance.GameOver(gameObject, didWin, UICanvas);

        if (!didWin)
        {
            #region Update High Score Table

            if (Game_Manager.Instance.startEnd.numTopScores == 5)
            {
                for (int i = 0; i < Game_Manager.Instance.startEnd.numTopScores; i++)
                {
                    if (score > int.Parse(Game_Manager.Instance.startEnd.score1stPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.score5thPlace = Game_Manager.Instance.startEnd.score1stPlace;
                        Game_Manager.Instance.startEnd.score1stPlace = "Player" + playerNumber + " " + score;
                    }
                    else if (score > int.Parse(Game_Manager.Instance.startEnd.score2ndPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.score5thPlace = Game_Manager.Instance.startEnd.score2ndPlace;
                        Game_Manager.Instance.startEnd.score2ndPlace = "Player" + playerNumber + " " + score;
                    }
                    else if (score > int.Parse(Game_Manager.Instance.startEnd.score3rdPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.score5thPlace = Game_Manager.Instance.startEnd.score3rdPlace;
                        Game_Manager.Instance.startEnd.score3rdPlace = "Player" + playerNumber + " " + score;
                    }
                    else if (score > int.Parse(Game_Manager.Instance.startEnd.score4thPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.score5thPlace = Game_Manager.Instance.startEnd.score4thPlace;
                        Game_Manager.Instance.startEnd.score4thPlace = "Player" + playerNumber + " " + score;
                    }
                    else if (score > int.Parse(Game_Manager.Instance.startEnd.score5thPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.score5thPlace = Game_Manager.Instance.startEnd.score5thPlace;
                        Game_Manager.Instance.startEnd.score5thPlace = "Player" + playerNumber + " " + score;
                    }
                }

                CmdSortAndAssignScores();
            }
            else
            {
                if (Game_Manager.Instance.startEnd.score1stPlace == "First 0000")
                {
                    Game_Manager.Instance.startEnd.score1stPlace = "Player" + playerNumber + " " + score;
                    Game_Manager.Instance.startEnd.numTopScores++;
                }
                else if (Game_Manager.Instance.startEnd.score2ndPlace == "Second 0000")
                {
                    Game_Manager.Instance.startEnd.score2ndPlace = "Player" + playerNumber + " " + score;
                    Game_Manager.Instance.startEnd.numTopScores++;
                }
                else if (Game_Manager.Instance.startEnd.score3rdPlace == "Third 0000")
                {
                    Game_Manager.Instance.startEnd.score3rdPlace = "Player" + playerNumber + " " + score;
                    Game_Manager.Instance.startEnd.numTopScores++;
                }
                else if (Game_Manager.Instance.startEnd.score4thPlace == "Fourth 0000")
                {
                    Game_Manager.Instance.startEnd.score4thPlace = "Player" + playerNumber + " " + score;
                    Game_Manager.Instance.startEnd.numTopScores++;
                }
                else
                {
                    Game_Manager.Instance.startEnd.score5thPlace = "Player" + playerNumber + " " + score;
                    Game_Manager.Instance.startEnd.numTopScores++;
                }

                CmdSortAndAssignScores();
            }

            #endregion
        }
        else
        {
            #region Update Best Time Table

            if (Game_Manager.Instance.startEnd.numTopTimes == 5)
            {
                for (int i = 0; i < Game_Manager.Instance.startEnd.numTopTimes; i++)
                {
                    if (Game_Manager.Instance.startEnd.gameTime > int.Parse(Game_Manager.Instance.startEnd.time1stPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.time5thPlace = Game_Manager.Instance.startEnd.time1stPlace;
                        Game_Manager.Instance.startEnd.time1stPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    }
                    else if (Game_Manager.Instance.startEnd.gameTime > int.Parse(Game_Manager.Instance.startEnd.time2ndPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.time5thPlace = Game_Manager.Instance.startEnd.time2ndPlace;
                        Game_Manager.Instance.startEnd.time2ndPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    }
                    else if (Game_Manager.Instance.startEnd.gameTime > int.Parse(Game_Manager.Instance.startEnd.time3rdPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.time5thPlace = Game_Manager.Instance.startEnd.time3rdPlace;
                        Game_Manager.Instance.startEnd.time3rdPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    }
                    else if (Game_Manager.Instance.startEnd.gameTime > int.Parse(Game_Manager.Instance.startEnd.time4thPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.time5thPlace = Game_Manager.Instance.startEnd.time4thPlace;
                        Game_Manager.Instance.startEnd.time3rdPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    }
                    else if (Game_Manager.Instance.startEnd.gameTime > int.Parse(Game_Manager.Instance.startEnd.time5thPlace.Split(' ')[1]))
                    {
                        Game_Manager.Instance.startEnd.time5thPlace = Game_Manager.Instance.startEnd.time5thPlace;
                        Game_Manager.Instance.startEnd.time5thPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    }
                }

                CmdSortAndAssignTimes();
            }
            else
            {
                if (Game_Manager.Instance.startEnd.time1stPlace == "First 0000")
                {
                    Game_Manager.Instance.startEnd.time1stPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    Game_Manager.Instance.startEnd.numTopTimes++;
                }
                else if (Game_Manager.Instance.startEnd.time2ndPlace == "Second 0000")
                {
                    Game_Manager.Instance.startEnd.time2ndPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    Game_Manager.Instance.startEnd.numTopTimes++;
                }
                else if (Game_Manager.Instance.startEnd.time3rdPlace == "Third 0000")
                {
                    Game_Manager.Instance.startEnd.time3rdPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    Game_Manager.Instance.startEnd.numTopTimes++;
                }
                else if (Game_Manager.Instance.startEnd.time4thPlace == "Fourth 0000")
                {
                    Game_Manager.Instance.startEnd.time4thPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    Game_Manager.Instance.startEnd.numTopTimes++;
                }
                else
                {
                    Game_Manager.Instance.startEnd.time5thPlace = "Player" + playerNumber + " " + Game_Manager.Instance.startEnd.gameTime;
                    Game_Manager.Instance.startEnd.numTopTimes++;
                }

                CmdSortAndAssignTimes();
            }

            #endregion
        }
    }

    public void ResetGame()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        startLight.SetActive(true);
        score = 0;
        health = 100;
        hasPickup = false;
        canMove = true;
        rb.velocity = Vector2.zero;
        Game_Manager.Instance.RestartUI();

        if (Game_Manager.Instance.playerHealth[0].name == "HealthBar")
        {
            Game_Manager.Instance.playerHealth[0].GetComponent<Image>().color = Color.green;
        }
        else
        {
            Game_Manager.Instance.playerHealth[1].GetComponent<Image>().color = Color.green;
        }
    }

    [Command]
    public void CmdSortAndAssignScores()
    {
        int[] scores = new int[5];

        scores[0] = int.Parse(Game_Manager.Instance.startEnd.score1stPlace.Split(' ')[1]);
        scores[1] = int.Parse(Game_Manager.Instance.startEnd.score2ndPlace.Split(' ')[1]);
        scores[2] = int.Parse(Game_Manager.Instance.startEnd.score3rdPlace.Split(' ')[1]);
        scores[3] = int.Parse(Game_Manager.Instance.startEnd.score4thPlace.Split(' ')[1]);
        scores[4] = int.Parse(Game_Manager.Instance.startEnd.score5thPlace.Split(' ')[1]);

        Array.Sort(scores);

        Game_Manager.Instance.startEnd.score1stPlace = "Player" + playerNumber + " " + scores[0];
        Game_Manager.Instance.startEnd.score2ndPlace = "Player" + playerNumber + " " + scores[1];
        Game_Manager.Instance.startEnd.score3rdPlace = "Player" + playerNumber + " " + scores[2];
        Game_Manager.Instance.startEnd.score4thPlace = "Player" + playerNumber + " " + scores[3];
        Game_Manager.Instance.startEnd.score5thPlace = "Player" + playerNumber + " " + scores[4];
    }

    [Command]
    public void CmdSortAndAssignTimes()
    {
        int[] times = new int[5];

        times[0] = int.Parse(Game_Manager.Instance.startEnd.time1stPlace.Split(' ')[1]);
        times[1] = int.Parse(Game_Manager.Instance.startEnd.time2ndPlace.Split(' ')[1]);
        times[2] = int.Parse(Game_Manager.Instance.startEnd.time3rdPlace.Split(' ')[1]);
        times[3] = int.Parse(Game_Manager.Instance.startEnd.time4thPlace.Split(' ')[1]);
        times[4] = int.Parse(Game_Manager.Instance.startEnd.time5thPlace.Split(' ')[1]);

        Array.Sort(times);

        Game_Manager.Instance.startEnd.time1stPlace = "Player" + playerNumber + " " + times[0];
        Game_Manager.Instance.startEnd.time2ndPlace = "Player" + playerNumber + " " + times[1];
        Game_Manager.Instance.startEnd.time3rdPlace = "Player" + playerNumber + " " + times[2];
        Game_Manager.Instance.startEnd.time4thPlace = "Player" + playerNumber + " " + times[3];
        Game_Manager.Instance.startEnd.time5thPlace = "Player" + playerNumber + " " + times[4];
    }

    public void SpawnPickupIcon()
    {
        if (isLocalPlayer)
        {
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                    createdIcon = Instantiate(fakePedestrianIcon, transform.position, initialRotation);
                    break;
                case 1:
                    currentPickup = Game_Manager.Pickup.HARPOON;
                    createdIcon = Instantiate(harpoonIcon, transform.position, initialRotation);
                    break;
                case 2:
                    currentPickup = Game_Manager.Pickup.KITTEN_CANNON;
                    createdIcon = Instantiate(kittenIcon, transform.position, initialRotation);
                    break;
                case 3:
                    currentPickup = Game_Manager.Pickup.SHIELD;
                    createdIcon = Instantiate(shieldIcon, transform.position, initialRotation);
                    break;
                default:
                    currentPickup = Game_Manager.Pickup.FAKE_PEDESTRIAN;
                    createdIcon = Instantiate(fakePedestrianIcon, transform.position, initialRotation);
                    break;
            }

            createdIcon.GetComponent<Pickup_Icon>().followTarget = gameObject;
            hasPickup = true;
        }
    }

    [Command]
    void CmdUsePickup(Game_Manager.Pickup pickup)
    {
        switch (pickup)
        {
            case Game_Manager.Pickup.FAKE_PEDESTRIAN:
                tempRotation = transform.rotation;
                transform.rotation = initialRotation;
                createdPickup = Instantiate(fakePedestrian, new Vector3(transform.position.x, transform.position.y - 3f, transform.position.z), transform.rotation);
                createdPickup.GetComponent<Fake_Pedestrian>().playerStart = this.gameObject;
                transform.rotation = tempRotation;
                break;
            case Game_Manager.Pickup.HARPOON:
                tempRotation = transform.rotation;
                transform.rotation = initialRotation;
                createdPickup = Instantiate(harpoon, new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z), transform.rotation, transform);
                createdPickup.GetComponent<Harpoon>().playerStart = this.gameObject;
                createdPickup.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                transform.rotation = tempRotation;
                break;
            case Game_Manager.Pickup.KITTEN_CANNON:
                tempRotation = transform.rotation;
                transform.rotation = initialRotation;
                createdPickup = Instantiate(kittenCannon, new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), transform.rotation, transform);
                createdPickup.GetComponent<Kitten_Cannon>().playerStart = this.gameObject;
                transform.rotation = tempRotation;
                break;
            case Game_Manager.Pickup.SHIELD:
                createdPickup = Instantiate(shield, new Vector3(transform.position.x, transform.position.y - 1.63f, transform.position.z), transform.rotation, transform);
                createdPickup.GetComponent<Shield_Animation_Create>().carToFollow = this.gameObject;
                break;
            default:
                break;
        }

        NetworkServer.Spawn(createdPickup);
    }

    public void CreateKittenFollow()
    {
        if (isLocalPlayer)
        {
            temp = Instantiate(kittenFollowPrefab);
            temp.GetComponent<Kitten_Follow>().followTarget = gameObject;
        }
    }

    [Command]
    public void CmdStartGame()
    {
        startLight = GameObject.FindGameObjectWithTag("StartLight");
        Game_Manager.Instance.startEnd.startGame = true;
    }

    [Command]
    public void CmdRestartGame()
    {
        if(Game_Manager.Instance.startEnd.restartGame)
        {
            Game_Manager.Instance.startEnd.restartGame = false;
        }
        else
        {
            Game_Manager.Instance.startEnd.restartGame = true;
            Game_Manager.Instance.startEnd.startGame = false;
        }
    }

    #endregion

    #region Collision Methods

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(((coll.gameObject.tag.Contains("Player")) || (coll.gameObject.tag.Contains("Racetrack"))) && canCollide)
        {
            health -= 10;
            collisionTimer = 60;
            canCollide = false;

            if (health <= 0)
            {
                CmdGameOver(false);
            }
        }
    }

    #endregion
}
