using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class ShopController : MonoBehaviour {
    public Jeu game;
    public GameObject container, scroll, descContainer;
    public GameObject[] legumesInstatiable;
    public Text periodePlantage;
    public Text tempsPousse;
    
    private GameObject[] legumesMenu;
    private GameObject legumeSelected, parcelle;
    private UIManager ui;

	// Use this for initialization
	void Start () {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        legumesMenu = new GameObject[9];
        RectTransform rt = (RectTransform)container.transform;

        float width = rt.rect.width;
        float height = rt.rect.height;

        for (int i = 0; i < game.Legumes.Count; i++) {
            if (game.Legumes[i].IdLegume < 7)
            {
                GameObject newLegume = Instantiate(legumesInstatiable[i], new Vector3(width / 2 + 50, container.transform.position.y - ((100 + 10) + 350 * (i + 2)), 0f), new Quaternion(0f, 0f, 0f, 0f));
                newLegume.GetComponent<LegumsMenuController>().IdLegume = game.Legumes[i].IdLegume;
                newLegume.GetComponent<LegumsMenuController>().Nom = game.Legumes[i].Nom;
                newLegume.GetComponent<LegumsMenuController>().Desc = game.GetDescription(game.Legumes[i].IdDescription);
                newLegume.transform.SetParent(container.transform);
                newLegume.GetComponent<Button>().onClick.AddListener(scroll.GetComponent<ScollViewController>().ScrollTo);
                newLegume.GetComponent<Button>().onClick.AddListener(ClickLegume);
                legumesMenu[i] = newLegume;
                if (i == 2)
                {
                    legumeSelected = legumesMenu[i];
                }
            }
		}
	}
	
	// Update is called once per frame
	void Update () {
        LegumsMenuController legume = legumeSelected.GetComponent<LegumsMenuController>();
        descContainer.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = legume.Desc.TexteDescription;
        descContainer.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = legume.Nom;
        
        DateTime tempsDep = DateTime.ParseExact(legume.Desc.DebutPeriodePlantage, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        DateTime tempsFin = DateTime.ParseExact(legume.Desc.FinPeriodePlantage, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        switch (game.Profil.IdNiveau)
        {
            case 1:
                periodePlantage.text = "Entre " + tempsDep.ToString("dd/MM", CultureInfo.InvariantCulture) + " et " + tempsFin.ToString("dd/MM", CultureInfo.InvariantCulture);
                tempsPousse.text = "Entre " + legume.Desc.TempsPousseMin + " et " + legume.Desc.TempsPousseMax + " jours";
                break;
            case 2:
                if (ui.GetSaison(tempsDep) == "printemps")
                    periodePlantage.text = "Au " + ui.GetSaison(tempsDep);
                else
                    periodePlantage.text = "En " + ui.GetSaison(tempsDep);

                int moyenne = (int) (legume.Desc.TempsPousseMin + legume.Desc.TempsPousseMax) / 2;
                tempsPousse.text = "Environ " + moyenne.ToString() + " jours";
                break;
            case 3:
                periodePlantage.text = "Indisponible";
                tempsPousse.text = "Indisponible";
                break;
        }
        
    }

	void ClickLegume(){
        legumeSelected = EventSystem.current.currentSelectedGameObject;
    }

    public GameObject[] getLegumesMenu()
    {
        return legumesMenu;
    }

    public void SetLegumeSelected(GameObject leg)
    {
        legumeSelected = leg;
    }

    public GameObject GetLegumeSelected()
    {
        return legumeSelected;
    }

    public GameObject Parcelle
    {
        get
        {
            return parcelle;
        }

        set
        {
            parcelle = value;
        }
    }
}
