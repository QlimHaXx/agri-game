using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PotageController : MonoBehaviour {

    public const int nbPointPourAgrandir = 2500;

    public GameObject parselle, fermier, plusXp;
    public GameObject[] legume;
    private bool parsellesShown = false;
    private bool parsellesPotageShown = false;
    private bool parsellesPotagePlantableShown = false;
    public Jeu jeu;
    private int nbCarreTerre = 0;
    private GameObject ui, shop;
    private DatabaseUpdate databaseUpdate = new DatabaseUpdate();

    // Use this for initialization
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI");
        RemplirPotager();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Shop") != null)
        {
            shop = GameObject.FindGameObjectWithTag("Shop");
        }
    }

    private void RemplirPotager()
    {
        for (int i = 0; i < jeu.Potager.Count; i++)
        {
            int idParcelle = -1; // c'est de l'herbe

            GameObject newParselle = Instantiate(parselle, new Vector3(jeu.Potager[i].PositionX, jeu.Potager[i].PositionY, 0f), new Quaternion(0f, 0f, 0f, 0f));
            newParselle.transform.parent = gameObject.transform;

            if (jeu.Potager[i].IdLegume == 8) // obligé car au chargement de la base on peut avoir des légumes plantés
                idParcelle = 0; // c'est de la terre
            else if (jeu.Potager[i].IdLegume != 7 && jeu.Potager[i].IdLegume != 8)
                idParcelle = jeu.Potager[i].IdLegume; // c'est un légume

            newParselle.GetComponent<ParselleController>().IdParselle = idParcelle;
            newParselle.GetComponent<ParselleController>().IdPotager = jeu.Potager[i].IdPotager;

            if (i > 3 && jeu.Potager[i].IdLegume != 7)
            {
                nbCarreTerre++;
            }

            if (idParcelle > 0) // on instancie que si c'est un légume
            {
                GameObject newLegume = Instantiate(legume[jeu.Potager[i].IdLegume-1], new Vector3(jeu.Potager[i].PositionX + 0.54f, jeu.Potager[i].PositionY, 0f), new Quaternion(0f, 0f, 0f, 0f));

                newLegume.transform.parent = newParselle.transform;

                newParselle.GetComponent<ParselleController>().Legume = newLegume;

                LegumsController legumeC = newLegume.GetComponent<LegumsController>();
                legumeC.IdLegume = idParcelle;
                legumeC.IdPotager = jeu.Potager[i].IdPotager; // on lui passe le numéro de la parcelle en base
                legumeC.TempsPousseMin = jeu.GetDescription(jeu.GetLegume(idParcelle).IdDescription).TempsPousseMin;
                legumeC.TempsPousseMax = jeu.GetDescription(jeu.GetLegume(idParcelle).IdDescription).TempsPousseMax;
                legumeC.DatePlantage = jeu.Potager[i].DatePlantage;
            }
        }
        if (nbCarreTerre >= 4)
        {
            nbCarreTerre -= 4;
        }
        else
            nbCarreTerre = 0;
    }

    //montre les parselles qu'on peut agrandir
    public void ShowOrHideParselles()
    {
        bool trouve = false;
        if (!parsellesPotageShown && !parsellesPotagePlantableShown) { // peut pas piocher si t'es en train d'arroser
            for (int i = 0; i < 48; i++)
            {
                GameObject newOne = gameObject.transform.GetChild(i).gameObject;
                ParselleController parselle = newOne.GetComponent<ParselleController>();

                if (parselle.APiocher())
                {
                    trouve = true;
                    if (!parsellesShown)
                        parselle.ShowCadre();
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                    else
                        parselle.HideCadre();
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                }
            }
            if (trouve)
                parsellesShown = !parsellesShown;
        }

    }

    //cache toutes les parcelles (quand on ouvre le shop par exemple)
    public void HideParselles()
    {
        for (int i = 0; i < 48; i++)
        {
            GameObject newOne = gameObject.transform.GetChild(i).gameObject;
            ParselleController parselle = newOne.GetComponent<ParselleController>();
            //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            parselle.HideCadre();
        }

        parsellesShown = parsellesPotageShown = parsellesPotagePlantableShown = false;
    }

    //montre les parselles qu'on peut arroser
    public void ShowOrHideParsellesPotager()
    {
        bool trouve = false;
        if (!parsellesShown && !parsellesPotagePlantableShown) // peut pas arroser si t'es en train de piocher ou en train de planter
        {
            for (int i = 0; i < 48; i++)
            {
                GameObject newOne = gameObject.transform.GetChild(i).gameObject;
                ParselleController parselle = newOne.GetComponent<ParselleController>();

                if (parselle.AArroser()) // un légume est planté
                {
                    trouve = true;
                    if (!parsellesPotageShown)
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                        parselle.ShowCadre();
                    else
                        parselle.HideCadre();
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                }
            }

            if(trouve)
                parsellesPotageShown = !parsellesPotageShown;
        }
    }

    // montre les parselles ou on peut planter
    public void ShowOrHideParsellesPotagerPlantable()
    {
        if (!parsellesPotageShown && !parsellesShown) {
            for (int i = 0; i < 48; i++)
            {
                GameObject newOne = gameObject.transform.GetChild(i).gameObject;
                ParselleController parselle = newOne.GetComponent<ParselleController>();

                if (parselle.APlantable()) // terre plantable
                {
                    if (!parsellesPotagePlantableShown)
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                        parselle.ShowCadre();
                    else
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                        parselle.HideCadre();
                }
            }

            parsellesPotagePlantableShown = !parsellesPotagePlantableShown;
        }
    }

    public void PlanterLegume()
    {
        LegumsMenuController legumeselected = shop.GetComponent<ShopController>().GetLegumeSelected().GetComponent<LegumsMenuController>();

        DateTime tempsDep = DateTime.ParseExact(legumeselected.Desc.DebutPeriodePlantage, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        DateTime tempsFin = DateTime.ParseExact(legumeselected.Desc.FinPeriodePlantage, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        if (ui.GetComponent<UIManager>().CompareDateJeu(ui.GetComponent<UIManager>().GetDateJeuDT(), tempsDep) == 0 ||
            ui.GetComponent<UIManager>().CompareDateJeu(ui.GetComponent<UIManager>().GetDateJeuDT(), tempsFin) == 2)
        {
            if (ui.GetComponent<UIManager>().Aide) {
                GameObject newFermier = Instantiate(fermier);
                newFermier.transform.SetParent(ui.gameObject.transform);
                newFermier.GetComponent<FermierController>().SetPhrasePasPlanter();
            }
            else
            {
                GameObject newXP = Instantiate(plusXp, new Vector3(0, 0, 0), new Quaternion(0f, 0f, 0f, 0f));
                databaseUpdate.UpdateProfilExp(jeu.Profil.Experience - 100);
                jeu.Profil.DateSortie = ui.GetComponent<UIManager>().GetDateJeu();
                databaseUpdate.UpdateProfilDate(jeu.Profil.DateSortie);
                newXP.GetComponent<PlusExpController>().SetMoinsXp(100);
            }
        }
        else
        {
            if(shop.GetComponent<ShopController>().Parcelle != null)
            {
                GameObject parcelle = shop.GetComponent<ShopController>().Parcelle;
                parcelle.GetComponent<ParselleController>().PlanterLegumeParcelle(this.gameObject);
            }
            else
            {
                ShowOrHideParsellesPotagerPlantable();
            }
        }
    }

    public bool Agandissable()
    {
        return (nbCarreTerre + 1) * nbPointPourAgrandir <= jeu.Profil.Experience;
    }

    // GETTERS AND SETTERS
    public bool ParsellesShown
    {
        get
        {
            return parsellesShown;
        }

        set
        {
            parsellesShown = value;
        }
    }

    public bool ParsellesPotageShown
    {
        get
        {
            return parsellesPotageShown;
        }

        set
        {
            parsellesPotageShown = value;
        }
    }

    public bool ParsellesPotagePlantableShown
    {
        get
        {
            return parsellesPotagePlantableShown;
        }

        set
        {
            parsellesPotagePlantableShown = value;
        }
    }

    public int NbCarreTerre
    {
        get
        {
            return nbCarreTerre;
        }

        set
        {
            nbCarreTerre = value;
        }
    }
}
