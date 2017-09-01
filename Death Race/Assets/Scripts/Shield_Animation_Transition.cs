using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Animation_Transition : MonoBehaviour
{
    public GameObject Shield_Close;
    int timer = 300;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer--;

        if (timer == 0)
        {
            Instantiate(Shield_Close, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
