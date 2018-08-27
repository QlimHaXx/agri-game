using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour {
    float duration = 0;
    Vector3 direction;
	// Use this for initialization
	void Start () {
         direction = new Vector3(100, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if ((int)duration % 10 == 0)
        {
            direction = new Vector3(-100, 0, 0);
        }
        else
        {
            transform.Translate(direction * Time.deltaTime);
        }
        duration += Time.deltaTime;
    }
}
