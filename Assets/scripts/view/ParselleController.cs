using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParselleController : MonoBehaviour {
    public GameObject[] mud;
    public GameObject[] bulles;
    public GameObject[] legumes, plusLegume;
    public GameObject plusXp, fermier;

    private GameObject jeu1;
    private Jeu jeu;
    private DatabaseUpdate databaseUpdate = new DatabaseUpdate();

    private GameObject legume, shop;
    private UIManager ui;
    private bool backgroundShown = false;
    private bool cadreShown = false;
    private int idParselle;
    private int idPotager;

    // Use this for initialization
    void Start () {        
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        jeu1 = GameObject.FindGameObjectWithTag("Game");
        jeu = jeu1.GetComponent<Jeu>();

        RemplirParselle();

        if (backgroundShown)
        {
            ShowFond();
        }

        UpdateFond();
    }
	
	// Update is called once per frame
	void Update ()
    {


        if (GameObject.FindGameObjectWithTag("Shop") != null){
            shop = GameObject.FindGameObjectWithTag("Shop");
        }

        UpdateFond();
	}

    private void UpdateFond()
    {
        if (idParselle == -1)
        {
            HideFond();
        }
        else
        {
            ShowFond();
        }
    }

    private void RemplirParselle()
    {
        float w = 0.64f;
        float h = -0.64f;
        float delta = 0.0f;

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject newMud = Instantiate(mud[UnityEngine.Random.Range(0, mud.Length)], new Vector3(transform.position.x + (j * w) - delta, transform.position.y + (i * h), 0f), new Quaternion(0f, 0f, 0f, 0f));
                newMud.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                newMud.transform.parent = gameObject.transform.Find("Fond");
            }
            delta += 0.32f;
        }
    }

    void OnMouseDown()
    {
        GameObject potage = gameObject.transform.parent.gameObject;

        if (!ui.GetComponent<UIManager>().GetBulleOpened() && !potage.GetComponent<PotageController>().ParsellesShown && !potage.GetComponent<PotageController>().ParsellesPotageShown)
        {
            if(idParselle >= 0) // si c'est de la terre
            {
                if (idParselle == 0) // pas de légumes
                {
                    if (!cadreShown)
                    {
                        GameObject newBulle = Instantiate(bulles[1], new Vector3(transform.position.x + 3f, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
                        newBulle.GetComponent<BulleController>().SetParselleClicked(gameObject);
                        newBulle.GetComponent<Canvas>().worldCamera = Camera.main;
                        ShowCadre();
                        ui.GetComponent<UIManager>().SetBulleOpened(true);
                    }
                    else // on plante
                    {
                        PlanterLegumeParcelle(potage);
                    }
                }
                else // ya un légume
                {
                    GameObject newBulle = Instantiate(bulles[0], new Vector3(transform.position.x + 3f, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
                    newBulle.GetComponent<BulleController>().SetParselleClicked(gameObject);
                    newBulle.GetComponent<Canvas>().worldCamera = Camera.main;
                    ShowCadre();
                    ui.GetComponent<UIManager>().SetBulleOpened(true);
                }
            }
        }
        else if (potage.GetComponent<PotageController>().ParsellesShown && APiocher()) // on veut piocher
        {
            if (potage.GetComponent<PotageController>().Agandissable())
            {
                idParselle = 0;
                Debug.Log(idPotager);
                potage.GetComponent<PotageController>().NbCarreTerre = potage.GetComponent<PotageController>().NbCarreTerre + 1;
                databaseUpdate.UpdatePotager(new Potager(idPotager, 8, null));
                HideCadre();
            }
            else
            {
                GameObject newFermier = Instantiate(fermier);
                newFermier.transform.SetParent(ui.gameObject.transform);
                newFermier.GetComponent<FermierController>().SetPhraseNonAgrandissable();
                potage.GetComponent<PotageController>().HideParselles();
            }

        }
        else if (potage.GetComponent<PotageController>().ParsellesPotageShown && AArroser()) // on veut arroser
        {
            Arroser();
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Bulle") != null)
            {
                GameObject.FindGameObjectWithTag("Bulle").GetComponent<BulleController>().Close();
            }
        }
        /*if (transform.GetChild(0).GetComponent<SpriteRenderer>().color.a > 0f)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            ShowFond();
        }*/
    }

    public void PlanterLegumeParcelle(GameObject potage)
    {
        GameObject legumeShop = shop.GetComponent<ShopController>().GetLegumeSelected();
        idParselle = legumeShop.GetComponent<LegumsMenuController>().IdLegume;
        GameObject newLegume = Instantiate(legumes[idParselle - 1], new Vector3(transform.position.x + 0.54f, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));

        LegumsController legumeC = newLegume.GetComponent<LegumsController>();
        legumeC.IdLegume = idParselle;
        legumeC.IdPotager = IdPotager;
        legumeC.TempsPousseMin = jeu.GetDescription(jeu.GetLegume(idParselle).IdDescription).TempsPousseMin;
        legumeC.TempsPousseMax = jeu.GetDescription(jeu.GetLegume(idParselle).IdDescription).TempsPousseMax;

        string datePlantage = ui.GetDateJeu();
        Debug.Log(datePlantage);
        legumeC.DatePlantage = datePlantage;

        newLegume.transform.parent = this.transform;

        legume = newLegume;


        databaseUpdate.UpdatePotager(new Potager(idPotager, idParselle, datePlantage));

        string dateSortie = ui.GetDateJeu();
        databaseUpdate.UpdateProfilDate(dateSortie);
        jeu.Profil.DateSortie = dateSortie;

        GameObject newXp = Instantiate(plusXp, new Vector3(transform.position.x + 0.54f, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
        newXp.GetComponent<PlusExpController>().SetXp(100);

        potage.GetComponent<PotageController>().HideParselles();
        ui.GetComponent<UIManager>().SetBulleOpened(false);

        if (potage.GetComponent<PotageController>().Agandissable())
        {
            GameObject newFermier = Instantiate(fermier);
            newFermier.transform.SetParent(ui.gameObject.transform);
            newFermier.GetComponent<FermierController>().SetPhraseNonAgrandissable();
        }
    }

    public void Recolter()
    {
        GameObject potage = gameObject.transform.parent.gameObject;
        int idLeg = idParselle;
        GameObject newXP = Instantiate(plusXp, new Vector3(transform.position.x, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
        GameObject newPlusLegume = Instantiate(plusLegume[idParselle - 1], new Vector3(transform.position.x, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
        Destroy(legume);
        idParselle = 0;

        UpdateBaseRecolter(idLeg);

        newXP.GetComponent<PlusExpController>().SetXp(500);
        ui.GetComponent<UIManager>().SetBulleOpened(false);

        if (potage.GetComponent<PotageController>().Agandissable())
        {
            GameObject newFermier = Instantiate(fermier);
            newFermier.transform.SetParent(ui.gameObject.transform);
            newFermier.GetComponent<FermierController>().SetPhraseNonAgrandissable();
        }
    }

    private void UpdateBaseRecolter(int idLeg)
    {
        Debug.Log(idLeg);
        databaseUpdate.UpdateProfilExp(jeu.Profil.Experience + 500);
        databaseUpdate.UpdatePotager(new Potager(idPotager, 8, null));
        Debug.Log(jeu.Recoltes[idLeg-1].NbRecoltes);
        jeu.Recoltes[idLeg-1].NbRecoltes += 1;
        databaseUpdate.UpdateNbLegumes(idLeg, jeu.Recoltes[idLeg-1].NbRecoltes, jeu.GetRecolte(idLeg).NbMalRecoltes);

        jeu.Profil.DateSortie = ui.GetComponent<UIManager>().GetDateJeu();
        databaseUpdate.UpdateProfilDate(jeu.Profil.DateSortie);
    }

    public void Arroser()
    {
        GameObject newXP = Instantiate(plusXp, new Vector3(transform.position.x, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
        databaseUpdate.UpdateProfilExp(jeu.Profil.Experience + 20);
        newXP.GetComponent<PlusExpController>().SetXp(20);
        GameObject potage = gameObject.transform.parent.gameObject;
        databaseUpdate.UpdateDateArrosage(idPotager, ui.GetDateJeu());
        potage.GetComponent<PotageController>().HideParselles();
        ui.GetComponent<UIManager>().SetBulleOpened(false);
    }

    public void Supprimer(int optionalXPMinus = 100, bool malRecolte = false)
    {
        if (jeu == null)
            jeu = GameObject.FindGameObjectWithTag("Game").GetComponent<Jeu>();
        int idLeg = idParselle;

        GameObject newXP = Instantiate(plusXp, new Vector3(transform.position.x, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
        newXP.GetComponent<PlusExpController>().SetMoinsXp(optionalXPMinus);
        //GameObject newPlusLegume = Instantiate(plusLegume[idParselle - 1], new Vector3(transform.position.x, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
        
        if (legume == null)
            legume = transform.parent.GetChild(2).gameObject;

        Destroy(legume);
        idParselle = 0;
        
        if (malRecolte)
        {
            jeu.Recoltes[idLeg-1].NbRecoltes += 1;
            jeu.Recoltes[idLeg-1].NbMalRecoltes += 1;
            databaseUpdate.UpdateNbLegumes(idLeg, jeu.Recoltes[idLeg-1].NbRecoltes, jeu.Recoltes[idLeg-1].NbMalRecoltes);    
        }

        if (databaseUpdate == null)
            databaseUpdate = new DatabaseUpdate();
        if (ui == null)
            ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        databaseUpdate.UpdateProfilExp(jeu.Profil.Experience - optionalXPMinus);
        databaseUpdate.UpdatePotager(new Potager(idPotager, 8, null));
        jeu.Profil.DateSortie = ui.GetComponent<UIManager>().GetDateJeu();
        databaseUpdate.UpdateProfilDate(jeu.Profil.DateSortie);
        ui.GetComponent<UIManager>().SetBulleOpened(false);
    }

    public void SupprimerLegume()
    {
        Supprimer(100, false);
    }

    void OnMouseOver()
    {
        if (transform.GetChild(0).GetComponent<SpriteRenderer>().color.a > 0f)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void OnMouseExit()
    {
        if (transform.GetChild(0).GetComponent<SpriteRenderer>().color.a > 0f)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }


    public void ShowCadre()
    {
        cadreShown = true;
        transform.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void HideCadre()
    {
        cadreShown = false;
        transform.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
    }

    public void ShowFond()
    {
        backgroundShown = true;
        for (int i = 0; i < 6; i++)
        {
            GameObject newOne = gameObject.transform.Find("Fond").GetChild(i).gameObject;
            newOne.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void HideFond()
    {
        backgroundShown = false;
        for (int i = 0; i < 6; i++)
        {
            GameObject newOne = gameObject.transform.Find("Fond").GetChild(i).gameObject;
            newOne.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void SetFond(bool fondin)
    {
        backgroundShown = fondin;
    }

    public bool GetFond()
    {
        return backgroundShown;
    }

    public bool APiocher()
    {
        return idParselle == -1 || idParselle == -2;
    }

    public bool APlantable()
    {
        return idParselle == 0;
    }

    public bool AArroser()
    {
        return idParselle > 0;
    }

    public bool ARetourner()
    {
        return idParselle == 2;
    }

    /// <summary>
    /// GETTERS AND SETTERS
    /// </summary>
    public int IdParselle
    {
        get
        {
            return idParselle;
        }

        set
        {
            idParselle = value;
        }
    }

    public GameObject Legume
    {
        get
        {
            return legume;
        }

        set
        {
            legume = value;
        }
    }

    public int IdPotager
    {
        get
        {
            return idPotager;
        }

        set
        {
            idPotager = value;
        }
    }
}
