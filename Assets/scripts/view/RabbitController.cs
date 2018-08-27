using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitController : MonoBehaviour {
    public GameObject text;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "carrot")
        {
            Vector3 spawnPosition = new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y, 0f);
            Instantiate(text, spawnPosition, gameObject.transform.rotation);
            Destroy(other.gameObject);            
        }
    }
}
