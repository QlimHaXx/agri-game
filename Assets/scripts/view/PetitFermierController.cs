using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetitFermierController : MonoBehaviour {
    private Camera cameraMain;
    private float life;
    private string recolter_deb = "Recolte le legume !"; // que si débutant
    private string manqueEau_deb = "Arrose le legume !"; // si debutant

    private GameObject ui;
    // Use this for initialization
    void Start () {
        ui = GameObject.FindGameObjectWithTag("UI");
        if (!ui.GetComponent<UIManager>().Aide)
        {
            Destroy(gameObject);
        }
        cameraMain = Camera.main;
        life = 4f;
    }
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;

        if (life < 0f)
        {
            Destroy(gameObject);
        }
    }

    public string Recolter_deb
    {
        get
        {
            return recolter_deb;
        }

        set
        {
            recolter_deb = value;
        }
    }

    public string ManqueEau_deb
    {
        get
        {
            return manqueEau_deb;
        }

        set
        {
            manqueEau_deb = value;
        }
    }

    public void SetPhraseManqueEauDeb()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = manqueEau_deb;
    }

    public void SetPhraseRecolterDeb()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = recolter_deb;
    }

}
