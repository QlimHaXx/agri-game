using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class LegumsController : MonoBehaviour
{
    public GameObject bulleAideDebutant, fermier;
    private Jeu jeu;

    private UIManager ui;
    private int idLegume; // -1 si herbe, -2 si terre non retournée, 0 si terre plantable, idLegume sinon
    private int idPotager;
    private int tempsPousseMin;
    private int tempsPousseMax;
    private DateTime dateSortie;
    private string datePlantage;

    // private Animator anim_eyes;
    private double tempsDePousseActuel;
    private int indexImage;
    private int tpsRepetition = 1000;
    private int nbActuel = 0;

    // Use this for initialization
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        jeu = GameObject.FindGameObjectWithTag("Game").GetComponent<Jeu>();

        //indexImage = 1;
        // anim_eyes = transform.GetComponentInChildren<Animator>();
        // anim_eyes.SetBool("Clicked", false);
    }

    // Update is called once per frame
    void Update()
    {
        /*count += Time.deltaTime;
        if ((int)count == 5)
        {
            anim_eyes.SetBool("Clicked", true);
            count = 0;
        }
        else
        {
            anim_eyes.SetBool("Clicked", false);
        }*/

        TimeSpan ts = DateTime.ParseExact(ui.GetDateJeu(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) - DateTime.ParseExact(datePlantage, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

        tempsDePousseActuel = ts.TotalHours;

        indexImage = (int) Decimal.Round((decimal)((tempsDePousseActuel * 5) / tempsPousseMin), 0);

        if (tempsDePousseActuel <= tempsPousseMin)
            ChangeImage(indexImage - 1);
        else
            ChangeImage(4);

        if (tempsDePousseActuel > (tempsPousseMin / 4) * (indexImage - 1))
        {
            //indexImage++;
            ChangeImage(indexImage-1);
        }

        /* if (tempsDePousseActuel <= tempsPousseMin)
        {
            tempsDePousseActuel += Time.deltaTime;
        }*/

        if (tempsDePousseActuel >= tempsPousseMin)
        {
            if (nbActuel % tpsRepetition == 0)
            {
                if (jeu.GetProfil(1).IdNiveau == 1)
                {
                    GameObject ferm = Instantiate(bulleAideDebutant, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), new Quaternion(0f, 0f, 0f, 0f));
                    ferm.GetComponent<PetitFermierController>().SetPhraseRecolterDeb();
                    //ferm.transform.SetParent(ui.transform);
                }
                else if (jeu.GetProfil(1).IdNiveau == 2)
                {
                    GameObject newFermier = Instantiate(fermier);
                    newFermier.transform.SetParent(ui.gameObject.transform);
                    newFermier.GetComponent<FermierController>().SetPhraseRecolterInt();
                }
            }
            nbActuel += 1;
        }

        if (tempsDePousseActuel > tempsPousseMax)
        {
            // TODO : faire perdre des points parce que légume pourrit
            ParselleController parcelle = gameObject.transform.parent.gameObject.GetComponent<ParselleController>();
            parcelle.Supprimer(500, true);
        }
    }

    public void ChangeImage(int index)
    {
        if (index > 0 && index < 5)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(index - 1).gameObject.SetActive(false);
            transform.GetChild(index).gameObject.SetActive(true);
        }
    }

    public int IdLegume
    {
        get
        {
            return idLegume;
        }

        set
        {
            idLegume = value;
        }
    }

    public int TempsPousseMin
    {
        get
        {
            return tempsPousseMin;
        }

        set
        {
            tempsPousseMin = value * 24;
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

    public int TempsPousseMax
    {
        get
        {
            return tempsPousseMax;
        }

        set
        {
            tempsPousseMax = value * 24;
        }
    }

    public DateTime DateSortie
    {
        get
        {
            return dateSortie;
        }

        set
        {
            dateSortie = value;
        }
    }

    public double TempsDePousseActuel
    {
        get
        {
            return tempsDePousseActuel;
        }

        set
        {
            tempsDePousseActuel = value;
        }
    }

    public string DatePlantage
    {
        get
        {
            return datePlantage;
        }

        set
        {
            datePlantage = value;
        }
    }
}