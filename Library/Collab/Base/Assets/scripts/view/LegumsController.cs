using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegumsController : MonoBehaviour
{
    public GameObject bulleAideDebutant;
    public Jeu jeu;

    private GameObject ui;
    private int idLegume; // -1 si herbe, -2 si terre non retournée, 0 si terre plantable, idLegume sinon
    private int idPotager;
    private int tempsPousseMin;
    private int tempsPousseMax;
    private DateTime dateSortie;

    // private Animator anim_eyes;
    private float tempsDePousseActuel;
    private int indexImage;
    private int tpsRepetition = 1000;
    private int nbActuel = 0;

    // Use this for initialization
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI");
        indexImage = 1;
        // anim_eyes = transform.GetComponentInChildren<Animator>();
        // anim_eyes.SetBool("Clicked", false);
        tempsDePousseActuel = 0;
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
        tempsPousseMin = 10;
        if (tempsDePousseActuel > (tempsPousseMin / 4) * indexImage)
        {
            indexImage++;
            ChangeImage(indexImage - 1);
        }

        if (tempsDePousseActuel <= tempsPousseMin)
        {
            tempsDePousseActuel += Time.deltaTime;
        }
        else if (tempsDePousseActuel >= tempsPousseMin)
        {
            if (nbActuel % tpsRepetition == 0)
            {
                GameObject ferm = Instantiate(bulleAideDebutant, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), new Quaternion(0f, 0f, 0f, 0f));
                ferm.GetComponent<PetitFermierController>().SetPhraseRecolterDeb();
                //ferm.transform.SetParent(ui.transform);
            }
            nbActuel += 1;
        }
        else if (tempsDePousseActuel > tempsPousseMax)
        {
            // TODO : faire perdre des points parce que légume pourrit
        }
    }

    public void ChangeImage(int index)
    {
        if (index > 0 && index < 5)
        {
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
            tempsPousseMin = value;
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
            tempsPousseMax = value;
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
}