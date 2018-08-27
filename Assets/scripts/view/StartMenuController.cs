using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour {
    public GameObject restartMenu;

	// Use this for initialization
	void Start () {
        //Debug.Log("start");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ContinuerPartie()
    {
        //Debug.Log("continuer");
        SceneManager.LoadScene("main");
    }

    public void RecommencerPartie()
    {
        //Debug.Log("Recommencer");
        restartMenu.SetActive(true);
    }

    public void Close(GameObject go)
    {
        go.SetActive(false);
    }
}
