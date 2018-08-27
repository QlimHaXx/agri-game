using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartController : MonoBehaviour {
    public GameObject toggle1, toggle2, toggle3, nameInput;
    DatabaseReset database = new DatabaseReset();
    DatabaseUpdate databaseUpdate = new DatabaseUpdate();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ValidationProfil()
    {
        InputField inputField = nameInput.GetComponent<InputField>();

        if (inputField.text != "" || (toggle1.GetComponent<Toggle>().isOn || toggle2.GetComponent<Toggle>().isOn || toggle3.GetComponent<Toggle>().isOn))
        {
            int idNiveau = 0;

            if (toggle1.GetComponent<Toggle>().isOn)
                idNiveau = 1;
            else if (toggle2.GetComponent<Toggle>().isOn)
                idNiveau = 2;
            else
                idNiveau = 3;

            databaseUpdate.UpdateProfil(new Profil(1, inputField.text, 0, "01/03/2018", 0, idNiveau));
            database.ResetDatabase();
            SceneManager.LoadScene("main");
            // faire update avec le numero de profil qui correspond (normalement 1 car on remet la base à 0)
        }
        else if (inputField.text == "")
        {
            // afficher l'input field en rouge + mettre un texte
        }
        else if (!toggle1.GetComponent<Toggle>().isOn && !toggle2.GetComponent<Toggle>().isOn && !toggle3.GetComponent<Toggle>().isOn)
        {
            // afficher les radiobouton en rouge + mettre un texte
        }
    }
}
