using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PotageController : MonoBehaviour {

    public const int nbPointPourAgrandir = 2500;

    public GameObject parselle, fermier;
    private bool parsellesShown = false;
    private bool parsellesPotageShown = false;
    private bool parsellesPotagePlantableShown = false;
    public Jeu jeu;
    private int nbCarreTerre = 0;
    private GameObject ui, shop;

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
            else if (jeu.Potager[i].IdLegume == 9) // c'est de la terre non retournee
                idParcelle = -2;

            newParselle.GetComponent<ParselleController>().IdParselle = idParcelle;
            newParselle.GetComponent<ParselleController>().IdPotager = jeu.Potager[i].IdPotager;

            if (idParcelle > 0) // on instancie que si c'est un légume
            {
                newParselle.GetComponent<LegumsController>().IdLegume = idParcelle;
                newParselle.GetComponent<LegumsController>().IdPotager = jeu.Potager[i].IdPotager; // on lui passe le numéro de la parcelle en base
            }
        }
    }

    //montre les parselles qu'on peut agrandir
    public void ShowOrHideParselles()
    {
        if (!parsellesPotageShown && !parsellesPotagePlantableShown) { // peut pas piocher si t'es en train d'arroser
            for (int i = 0; i < 48; i++)
            {
                GameObject newOne = gameObject.transform.GetChild(i).gameObject;
                ParselleController parselle = newOne.GetComponent<ParselleController>();

                if (parselle.APiocher())
                {
                    if (!parsellesShown)
                        parselle.ShowCadre();
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                    else
                        parselle.HideCadre();
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                }
            }

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
        if (!parsellesShown && !parsellesPotagePlantableShown) // peut pas arroser si t'es en train de piocher ou en train de planter
        {
            for (int i = 0; i < 48; i++)
            {
                GameObject newOne = gameObject.transform.GetChild(i).gameObject;
                ParselleController parselle = newOne.GetComponent<ParselleController>();

                if (parselle.AArroser()) // un légume est planté
                {
                    if (!parsellesPotageShown)
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                        parselle.ShowCadre();
                    else
                        parselle.HideCadre();
                        //newOne.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
                }
            }

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

        //Debug.Log(tempsDep);
        //Debug.Log(ui.GetComponent<UIManager>().CompareDateJeu(ui.GetComponent<UIManager>().GetDateJeuDT(), tempsDep));

        if (ui.GetComponent<UIManager>().CompareDateJeu(ui.GetComponent<UIManager>().GetDateJeuDT(), tempsDep) == 0 ||
            ui.GetComponent<UIManager>().CompareDateJeu(ui.GetComponent<UIManager>().GetDateJeuDT(), tempsFin) == 2)
        {
            GameObject newFermier = Instantiate(fermier);
            newFermier.transform.SetParent(ui.gameObject.transform);
            newFermier.GetComponent<FermierController>().SetPhrasePasPlanter();
        }
        else
        {
            ShowOrHideParsellesPotagerPlantable();
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
