using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Drop : MonoBehaviour
{
    public GameObject Bomb;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Bomb, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
	}
}
