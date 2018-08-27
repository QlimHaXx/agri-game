using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangerNiveauController : MonoBehaviour {

    public GameObject toggle1, toggle2, toggle3;
    DatabaseUpdate databaseUpdate = new DatabaseUpdate();
    private Jeu jeu;
    private UIManager ui;

    // Use this for initialization
    void Start () {
		jeu = GameObject.FindGameObjectWithTag("Game").GetComponent<Jeu>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        switch (jeu.Profil.IdNiveau)
        {
            case 1:
                toggle1.GetComponent<Toggle>().isOn = true;
                break;
            case 2:
                toggle2.GetComponent<Toggle>().isOn = true;
                break;
            case 3:
                toggle3.GetComponent<Toggle>().isOn = true;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangerNiveau()
    {
        if ((toggle1.GetComponent<Toggle>().isOn || toggle2.GetComponent<Toggle>().isOn || toggle3.GetComponent<Toggle>().isOn))
        {
            int idNiveau = 0;

            if (toggle1.GetComponent<Toggle>().isOn)
                idNiveau = 1;
            else if (toggle2.GetComponent<Toggle>().isOn)
                idNiveau = 2;
            else
                idNiveau = 3;

            jeu.Profil.IdNiveau = idNiveau;
            databaseUpdate.UpdateProfilNiveau(1, idNiveau);

            if (idNiveau < 3)
                ui.Aide = true;
        }
    }
}
