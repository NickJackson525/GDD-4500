using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitten_Follow : MonoBehaviour
{
    enum SwerveDirection { LEFT, RIGHT }
    public GameObject followTarget;
    int sequenceCount = 0;
    int sequenceGoal = 4;
    SwerveDirection nextDirection = SwerveDirection.LEFT;
    public Vector3 targetPosition;
    float speed = 30f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        targetPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, -1f);

        if(followTarget.tag == "Player1")
        {
            if ((Input.GetKeyDown (KeyCode.A)) && (nextDirection == SwerveDirection.LEFT))
            {
                sequenceCount++;
                nextDirection = SwerveDirection.RIGHT;
                //targetPosition = new Vector3(followTarget.transform.position.x - 3f, followTarget.transform.position.y, followTarget.transform.position.z);
            }
            if ((Input.GetKeyDown(KeyCode.D)) && (nextDirection == SwerveDirection.RIGHT))
            {
                sequenceCount++;
                nextDirection = SwerveDirection.LEFT;
                //targetPosition = new Vector3(followTarget.transform.position.x + 3f, followTarget.transform.position.y, followTarget.transform.position.z);
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (nextDirection == SwerveDirection.LEFT))
            {
                sequenceCount++;
                nextDirection = SwerveDirection.RIGHT;
                //targetPosition = new Vector3(followTarget.transform.position.x - 3f, followTarget.transform.position.y, followTarget.transform.position.z);
            }
            if ((Input.GetKeyDown(KeyCode.RightArrow)) && (nextDirection == SwerveDirection.RIGHT))
            {
                sequenceCount++;
                nextDirection = SwerveDirection.LEFT;
                //targetPosition = new Vector3(followTarget.transform.position.x + 3f, followTarget.transform.position.y, followTarget.transform.position.z);
            }
        }

        transform.position = targetPosition;

        if(sequenceCount >= sequenceGoal)
        {
            Destroy(this.gameObject);
        }
	}
}
