using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScollViewController : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IScrollHandler
{
    public GameObject shop;

    private GameObject legumeSelected;
    private ScrollRect sr;
    private bool scrolling;
    private float scrollValue, animDuration, scrollSpeed, initScrollValue;
    // Use this for initialization
    void Start () {
        animDuration = initScrollValue = 0f;
        scrollSpeed = 0.015f;
        scrolling = false;
        sr = GetComponent<ScrollRect>();        
    }
	
	// Update is called once per frame
	void Update () {
        if (scrolling)
        {
            if (Mathf.Abs(animDuration) < Mathf.Abs(scrollValue) - scrollSpeed)
            {
                if(scrollValue / Mathf.Abs(scrollValue) > 0)
                {
                    sr.verticalNormalizedPosition -= scrollSpeed;
                }
                else
                {
                    sr.verticalNormalizedPosition += scrollSpeed;
                }
                
                animDuration += scrollSpeed;
            }
            else
            {
                animDuration = 0f;
                scrolling = false;
            }
        }
    }

    public void ScrollTo()
    {
        legumeSelected = EventSystem.current.currentSelectedGameObject;
        shop.GetComponent<ShopController>().SetLegumeSelected(legumeSelected);
        scrollValue = Screen.height / 2 - EventSystem.current.currentSelectedGameObject.transform.position.y;
        scrollValue = scrollValue / 2400;
        scrolling = true;
    }

    public void ScrollToVal(float value)
    {
        scrollValue = value;
        scrolling = true;
    }

    //Do this when the user stops dragging this UI Element.
    public void OnBeginDrag(PointerEventData data)
    {
        initScrollValue = sr.verticalNormalizedPosition;
    }
    //Do this when the user stops dragging this UI Element.
    public void OnEndDrag(PointerEventData data)
    {
        float draggedValue = initScrollValue - sr.verticalNormalizedPosition;

        if (draggedValue < 0)
        {
            //Dragged Down
        }
        else
        {
            //Dragged Up
            
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        /*for (int i = 0; i < shop.GetComponent<ShopController>().getLegumesMenu().Length; i++)
        {
            if (Screen.height / 2 - shop.GetComponent<ShopController>().getLegumesMenu()[i].transform.position.y < Screen.height / 2 + 100 &&
                Screen.height / 2 - shop.GetComponent<ShopController>().getLegumesMenu()[i].transform.position.y > Screen.height / 2 - 100)
            {
                legumeSelected = shop.GetComponent<ShopController>().getLegumesMenu()[i];
                Debug.Log(legumeSelected.transform.position.y);
            }
        }*/
    }

    public GameObject getLegumSelected()
    {
        return legumeSelected;
    }
}
