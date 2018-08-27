using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour {
    public Jeu game;
    public GameObject legume, container, scroll, descContainer;

    private GameObject[] legumesMenu;
    private GameObject legumeSelected;

	/*public static T GetCopyOf<T>(this Component comp, T other) where T : Component
	{
		Type type = comp.GetType();
		if (type != other.GetType()) return null; // type mis-match
		BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
		PropertyInfo[] pinfos = type.GetProperties(flags);
		foreach (var pinfo in pinfos) {
			if (pinfo.CanWrite) {
				try {
					pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
				}
				catch { } // In case of NotImplementedException being thrown. For some reason specifying that exception didn't seem to catch it, so I didn't catch anything specific.
			}
		}
		FieldInfo[] finfos = type.GetFields(flags);
		foreach (var finfo in finfos) {
			finfo.SetValue(comp, finfo.GetValue(other));
		}
		return comp as T;
	}*/

	// Use this for initialization
	void Start () {
        legumesMenu = new GameObject[8];
        RectTransform rt = (RectTransform)container.transform;

        float width = rt.rect.width;
        float height = rt.rect.height;

        for (int i = 0; i < game.Legumes.Length; i++) {
            if (game.Legumes[i].IdLegume < 7)
            {
                GameObject newLegume = Instantiate(legume, new Vector3(width / 2 + 50, container.transform.position.y - ((100 + 10) + 350 * (i + 2)), 0f), new Quaternion(0f, 0f, 0f, 0f));
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
        descContainer.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = legumeSelected.GetComponent<LegumsMenuController>().Desc.TexteDescription;
        descContainer.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = legumeSelected.GetComponent<LegumsMenuController>().Nom;
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
}
