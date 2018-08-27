using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Globalization;

public class UIManager : MonoBehaviour {
    public Jeu jeu;
    public GameObject shopButton, shopScreen, optionScreen, background, time, date, scoreText, seasonButton, fermier, profilOption, potage, menuOptions;
    public GameObject[] grass;
    public Sprite automn, spring, winter, summer;
    

    private float hour, min, jour;
    private int score;
    private string hourText, minText;
    private int mois;
    private Dictionary<int, KeyValuePair<string, int>> tabMois;
    private bool bulleOpened, menuOpened, fermier_ouvert, aide;
    private int tpsRepetition = 1000;
    private int nbActuel = 0;

    // Use this for initialization
    void Start ()
    {
        bulleOpened = menuOpened = fermier_ouvert = false;
        RemplirCarteHerbe();

        RemplirTemps();

        score = jeu.Profil.Experience;

        aide = true;
        menuOptions.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Desactiver l'aide";
    }

    private void RemplirTemps()
    {
        //Debug.Log(jeu.Profil.DateSortie);
        DateTime dt = DateTime.ParseExact(jeu.Profil.DateSortie, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        
        hour = dt.Hour;
        min = dt.Minute;

        hourText = hour.ToString();
        minText = min.ToString();

        jour = dt.Day;
        mois = dt.Month;

        tabMois = new Dictionary<int, KeyValuePair<string, int>>();
        tabMois.Add(1, new KeyValuePair<string, int>("jan.", 31));
        tabMois.Add(2, new KeyValuePair<string, int>("fév.", 28));
        tabMois.Add(3, new KeyValuePair<string, int>("mar.", 31));
        tabMois.Add(4, new KeyValuePair<string, int>("avr.", 30));
        tabMois.Add(5, new KeyValuePair<string, int>("mai", 31));
        tabMois.Add(6, new KeyValuePair<string, int>("jun.", 30));
        tabMois.Add(7, new KeyValuePair<string, int>("jui.", 31));
        tabMois.Add(8, new KeyValuePair<string, int>("aoü.", 31));
        tabMois.Add(9, new KeyValuePair<string, int>("sep.", 30));
        tabMois.Add(10, new KeyValuePair<string, int>("oct.", 31));
        tabMois.Add(11, new KeyValuePair<string, int>("nov.", 30));
        tabMois.Add(12, new KeyValuePair<string, int>("déc.", 31));

        time.GetComponent<TextMeshProUGUI>().text = hourText + ":" + minText;
        date.GetComponent<TextMeshProUGUI>().text = jour + " " + tabMois[mois].Key;
        scoreText.GetComponent<TextMeshProUGUI>().text = "" + score;

        switch (mois)
        {
            case 3:
                if (jour < 21)
                    seasonButton.GetComponent<Image>().sprite = winter;
                if (jour >=21)
                    seasonButton.GetComponent<Image>().sprite = spring;
                break;
            case 4:
            case 5:
                seasonButton.GetComponent<Image>().sprite = spring;
                break;
            case 6:
                if (jour < 21)
                    seasonButton.GetComponent<Image>().sprite = spring;
                if (jour >= 21)
                    seasonButton.GetComponent<Image>().sprite = summer;
                break;
            case 7:
            case 8:
                seasonButton.GetComponent<Image>().sprite = summer;
                break;
            case 9:
                if (jour < 21)
                    seasonButton.GetComponent<Image>().sprite = summer;
                if (jour >= 21)
                    seasonButton.GetComponent<Image>().sprite = automn;
                break;
            case 10:
            case 11:
                seasonButton.GetComponent<Image>().sprite = automn;
                break;
            case 12:
                if (jour < 21)
                    seasonButton.GetComponent<Image>().sprite = automn;
                if (jour >= 21)
                    seasonButton.GetComponent<Image>().sprite = winter;
                break;
            case 1:
            case 2:
                seasonButton.GetComponent<Image>().sprite = winter;
                break;
        }

        //seasonButton.GetComponent<Image>().sprite = winter;
    }

    // Update is called once per frame
    void Update ()
    {
        if(jeu.Profil.IdNiveau == 3)
        {
            aide = false;
            menuOptions.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Aide indisponible";
        }
        else
        {
            if (aide)
                menuOptions.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Desactiver aide";
            else
                menuOptions.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Activer aide";

        }
        if (potage.GetComponent<PotageController>().Agandissable())// && fermier_ouvert)
        {
            if (nbActuel % tpsRepetition == 0)
            {
                GameObject newFermier = Instantiate(fermier);
                newFermier.transform.SetParent(gameObject.transform.parent);
                newFermier.GetComponent<FermierController>().SetPhraseAgrandir();
                newFermier.transform.GetChild(1).gameObject.SetActive(true);
                fermier_ouvert = false;
            }
            nbActuel += 1;
        }

        if (Input.GetButtonUp("Cancel"))
        {
            if (bulleOpened)
            {
                GameObject.FindGameObjectWithTag("Bulle").GetComponent<BulleController>().Close();
            }

            if (menuOpened)
            {
                Close(optionScreen);
                Close(shopScreen);
            }
        }

        MettreAJourTemps();
    }

    private void RemplirCarteHerbe()
    {
        float w = 0.64f;
        float h = -0.64f;
        float delta = 0.0f;

        for (int i = -14; i < 30; i++)
        {
            for (int j = -20; j < 70; j++)
            {
                GameObject newMud = Instantiate(grass[UnityEngine.Random.Range(0, grass.Length)], new Vector3(-9 + (j * w) - delta, 5 + (i * h), 0), new Quaternion(0, 0, 0, 0));
                newMud.transform.parent = background.transform;
            }
            delta += 0.32f;
        }
    }

    private void MettreAJourTemps()
    {

        if(jour > tabMois[mois].Value)
        {
            jour = 1;
            mois += 1;
        }

        if (min < 60)
        {
            min += Time.deltaTime * 50;
        }
        else
        {
            hour++;
            min = 0;
        }
        if (hour == 24)
        {
            jour++;
            hour = 0;
        }

        if (min < 10)
        {
            minText = "0" + (int)min;
        }
        else
        {
            minText = "" + (int)min;
        }

        if (hour < 10)
        {
            hourText = "0" + hour;
        }
        else
        {
            hourText = "" + hour;
        }

        if (jour == 21  && (mois == 3 || mois == 6 || mois == 9 || mois == 12))
        {
            switch (mois)
            {
                case 3:
                    seasonButton.GetComponent<Image>().sprite = spring;
                    break;
                case 6:
                    seasonButton.GetComponent<Image>().sprite = summer;
                    break;
                case 9:
                    seasonButton.GetComponent<Image>().sprite = automn;
                    break;
                case 12:
                    seasonButton.GetComponent<Image>().sprite = winter;
                    break;
            }

        }
        
        time.GetComponent<TextMeshProUGUI>().text = hourText + ":00";
        date.GetComponent<TextMeshProUGUI>().text = jour + " " + tabMois[mois].Key;
        scoreText.GetComponent<TextMeshProUGUI>().text = "" + score;
    }

    public void Open(GameObject go)
    {
        if (bulleOpened)
        {
            GameObject.FindGameObjectWithTag("Bulle").GetComponent<BulleController>().Close();
        }
        bulleOpened = true;
        menuOpened = true;
        go.SetActive(true);
    }
    public void Close(GameObject go)
    {
        bulleOpened = false;
        menuOpened = false;
        go.SetActive(false);
    }

    public void AddScore(int val)
    {
        score += val;
        jeu.Profil.Experience += val;
    }

    public void HoeClick()
    {

    }

    public void SetBulleOpened(bool in_)
    {
        bulleOpened = in_;
    }

    public bool GetBulleOpened()
    {
        return bulleOpened;
    }

    public void AfficheFermier()
    {
        GameObject newFermier = Instantiate(fermier);
        newFermier.transform.SetParent(gameObject.transform.parent);
    }

    public string GetDateJeu()
    {
        string date = "";

        if (jour < 10)
            if (mois < 10)
                date = "0" + jour + "/0" + mois;
            else
                date = "0" + jour + "/" + mois;
        else
            if (mois < 10)
                date = jour + "/0" + mois;
            else
                date = jour + "/" + mois;

        return date + "/2018 " + hourText + ":00";
    }
    
    public DateTime GetDateJeuDT(){
        DateTime dt = DateTime.ParseExact(GetDateJeu(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        return dt;
    }

    public int CompareDateJeu(DateTime date1, DateTime date2)
    {
        int jour1 = date1.Day;
        int mois1 = date1.Month;
        int heure1 = date1.Hour;

        int jour2 = date2.Day;
        int mois2 = date2.Month;
        int heure2 = date2.Hour;

        if (mois1 < mois2)
        {
            return 0; // date 1 plus petite
        }
        else if (mois1 == mois2)
        {
            if(jour1 < jour2)
            {
                return 0;
            }
            else if(jour1 == jour2)
            {
                if (heure1 < heure2)
                    return 0;
                else if (heure1 == heure2)
                    return 1; // dates egales
                else
                    return 2; // date 2 plus petite
            }
            else
            {
                return 2;
            }
        }
        else
        {
            return 2;
        }
    }

    public string GetSaison(DateTime date)
    {
        int day = date.Day;
        int month = date.Month;


        switch (mois)
        {
            case 3:
                if (jour < 21)
                    return "hiver";
                if (jour >= 21)
                    return "printemps";
                break;
            case 4:
            case 5:
                return "printemps";
            case 6:
                if (jour < 21)
                    return "printemps";
                if (jour >= 21)
                    return "été";
                break;
            case 7:
            case 8:
                return "été";
            case 9:
                if (jour < 21)
                    return "été";
                if (jour >= 21)
                    return "autumn";
                break;
            case 10:
            case 11:
                return "autumn";
            case 12:
                if (jour < 21)
                    return "autumn";
                if (jour >= 21)
                    return "hiver";
                break;
            case 1:
            case 2:
                return "hiver";
        }
            
        return "";
    }

    public bool Fermier_ouvert
    {
        get
        {
            return fermier_ouvert;
        }

        set
        {
            fermier_ouvert = value;
        }
    }

    public bool Aide
    {
        get
        {
            return aide;
        }
        set
        {
            aide = value;
        }
    }

    public void SetAide()
    {
        if(jeu.Profil.IdNiveau == 3)
        {
            return;
        }
        if (!aide)
        {
            menuOptions.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Desactiver l'aide";
            aide = true;
        }
        else
        {
            menuOptions.transform.GetChild(1).GetChild(2).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "Activer l'aide";
            aide = false;
        }
    }
}
