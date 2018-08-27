using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulleController : MonoBehaviour {
    public GameObject fermier;
    private GameObject parselleClicked;
    private GameObject ui;

    private string phraseBulle;

    // Use this for initialization
    void Start () {
        ui = GameObject.FindGameObjectWithTag("UI");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Close()
    {
        parselleClicked.GetComponent<ParselleController>().HideCadre();
        ui.GetComponent<UIManager>().SetBulleOpened(false);
        //ui.GetComponent<UIManager>().OpenShop();
        Destroy(gameObject);
    }

    public void Planter()
    {
        parselleClicked.GetComponent<ParselleController>().HideCadre();
        ui.GetComponent<UIManager>().Open(ui.GetComponent<UIManager>().shopScreen);
        ui.GetComponent<UIManager>().shopScreen.GetComponent<ShopController>().Parcelle = parselleClicked;
        Destroy(gameObject);
    }

    public void SupprimerLegume()
    {
        parselleClicked.GetComponent<ParselleController>().SupprimerLegume();
        parselleClicked.GetComponent<ParselleController>().HideCadre();
        ui.GetComponent<UIManager>().SetBulleOpened(false);
        Destroy(gameObject);
    }

    public void Recolter()
    {
        GameObject legume = parselleClicked.GetComponent<ParselleController>().Legume;

        if (legume.GetComponent<LegumsController>().TempsDePousseActuel < legume.GetComponent<LegumsController>().TempsPousseMin)
        {
            GameObject newFermier = Instantiate(fermier);
            newFermier.transform.SetParent(ui.gameObject.transform);
            newFermier.GetComponent<FermierController>().SetPhrasePasRecolter();
            parselleClicked.GetComponent<ParselleController>().HideCadre();
            ui.GetComponent<UIManager>().SetBulleOpened(false);
            Destroy(gameObject);
        }
        else
        {
            parselleClicked.GetComponent<ParselleController>().HideCadre();
            parselleClicked.GetComponent<ParselleController>().Recolter();
            ui.GetComponent<UIManager>().SetBulleOpened(false);
            Destroy(gameObject);
        }
    }

    public void SetParselleClicked(GameObject in_)
    {
        parselleClicked = in_;
    }

    public void AfficherFermier()
    {
        parselleClicked.GetComponent<ParselleController>().HideCadre();
        ui.GetComponent<UIManager>().AfficheFermier();
        ui.GetComponent<UIManager>().SetBulleOpened(false);
        Destroy(gameObject);
    }

    public void Arroser()
    {
        parselleClicked.GetComponent<ParselleController>().HideCadre();
        parselleClicked.GetComponent<ParselleController>().Arroser();
        ui.GetComponent<UIManager>().SetBulleOpened(false);
        Destroy(gameObject);
    }

    public string PhraseBulle
    {
        get
        {
            return phraseBulle;
        }

        set
        {
            phraseBulle = value;
        }
    }
}
