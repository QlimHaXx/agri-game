using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfilController : MonoBehaviour {
    public Jeu jeu;
    public Text textNom, nbBien, nbMal, nbRecolte;

	// Use this for initialization
	void Start () {
        textNom.text = jeu.Profil.Nom;

        int nbRecoltes = 0, nbBienRecoltes = 0, nbMalRecoltes = 0;

        for (int i = 0; i < jeu.Recoltes.Count; i++)
        {
            nbMalRecoltes += jeu.Recoltes[i].NbMalRecoltes;
            nbRecoltes += jeu.Recoltes[i].NbRecoltes;
        }

        nbBienRecoltes = nbRecoltes - nbMalRecoltes;

        nbBien.text = nbBienRecoltes.ToString();
        nbMal.text = nbMalRecoltes.ToString();

        nbRecolte.text = nbRecoltes.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
