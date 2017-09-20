using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Animation_Close : MonoBehaviour
{
    string currentSprite;
    public GameObject carToFollow;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentSprite = GetComponent<SpriteRenderer>().sprite.name;

        if (currentSprite == "Shield_19")
        {
            carToFollow.GetComponent<Car_Controller>().hasShield = false;
            Destroy(this.gameObject);
        }
        
        this.transform.position = new Vector3(carToFollow.transform.position.x, carToFollow.transform.position.y, -1);
        this.transform.rotation = carToFollow.transform.rotation;
        this.transform.Rotate(0, 0, 180);
    }
}
