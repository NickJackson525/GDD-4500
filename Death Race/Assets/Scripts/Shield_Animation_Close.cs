using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Animation_Close : MonoBehaviour
{
    string currentSprite;

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
            Destroy(this.gameObject);
        }
    }
}
