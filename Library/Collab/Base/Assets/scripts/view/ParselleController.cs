using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParselleController : MonoBehaviour {
    public GameObject[] mud;
    public GameObject[] bulles;
    public GameObject[] legumes, plusLegume;
    public GameObject plusXp;
    private GameObject jeu1;
    private Jeu jeu;

    private GameObject ui, legume, shop;
    private bool backgroundShown = false;
    private bool cadreShown = false;
    private int idParselle;
    private int idPotager;

    // Use this for initialization
    void Start () {        
        ui = GameObject.FindGameObjectWithTag("UI");
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
        if(GameObject.FindGameObjectWithTag("Shop") != null){
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
                        GameObject legumeShop = shop.GetComponent<ShopController>().GetLegumeSelected();
                        idParselle = legumeShop.GetComponent<LegumsMenuController>().IdLegume;
                        GameObject newLegume = Instantiate(legumes[idParselle-1], new Vector3(transform.position.x + 0.54f, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
                        legume = newLegume;

                        newLegume.GetComponent<LegumsController>().IdLegume = idParselle;
                        newLegume.GetComponent<LegumsController>().IdPotager = IdPotager;
                        newLegume.GetComponent<LegumsController>().TempsPousseMin = jeu.GetDescription(jeu.GetLegume(idParselle).IdDescription).TempsPousseMin;
                        newLegume.GetComponent<LegumsController>().TempsPousseMax = jeu.GetDescription(jeu.GetLegume(idParselle).IdDescription).TempsPousseMax;
                        //TODO : newLegume.GetComponent<LegumsController>().DateSortie = ui.GetDateJeu();

                        GameObject newXp = Instantiate(plusXp, new Vector3(transform.position.x + 0.54f, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
                        newXp.GetComponent<PlusExpController>().SetXp(100);

                        potage.GetComponent<PotageController>().HideParselles();
                        ui.GetComponent<UIManager>().SetBulleOpened(false);
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
                potage.GetComponent<PotageController>().NbCarreTerre = potage.GetComponent<PotageController>().NbCarreTerre + 1;
                HideCadre();
            }
            else
            {
                Debug.Log("Terrain non agrandissable, vous pouvez l'agrandir tous les 2500 points");
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

    public void Recolter()
    {
        GameObject newXP = Instantiate(plusXp, new Vector3(transform.position.x, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
        GameObject newPlusLegume = Instantiate(plusLegume[idParselle-1], new Vector3(transform.position.x, transform.position.y), new Quaternion(0f, 0f, 0f, 0f));
        Destroy(legume);
        idParselle = 0;
        // TODO : MEttre a jour la base
        newXP.GetComponent<PlusExpController>().SetXp(500);
        ui.GetComponent<UIManager>().SetBulleOpened(false);
    }

    public void Arroser()
    {
        // TODO: mettre à jour la dernière date d'arrosage en base
        GameObject potage = gameObject.transform.parent.gameObject;
        potage.GetComponent<PotageController>().HideParselles();
        ui.GetComponent<UIManager>().SetBulleOpened(false);
        Debug.Log("ARRRRRROSSAGE");
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
            return Legume;
        }

        set
        {
            Legume = value;
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
