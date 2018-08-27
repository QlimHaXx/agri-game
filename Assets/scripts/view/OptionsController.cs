using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour {
    public GameObject restartMenu, profil, changerNiveau;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OuvrirProfil()
    {
        profil.SetActive(true);
    }

    public void RecommencerPartie()
    {
        //Debug.Log("Recommencer");
        restartMenu.SetActive(true);
    }

    public void ChangerNiveau()
    {
        changerNiveau.SetActive(true);
    }
}
