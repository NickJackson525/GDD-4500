using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Animation_Create : MonoBehaviour
{
    public GameObject Shield_Full;
    string currentSprite;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentSprite = GetComponent<SpriteRenderer>().sprite.name;

        if (currentSprite == "Shield_10")
        {
            Instantiate(Shield_Full, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
	}
}
