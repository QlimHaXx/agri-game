using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolsController : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 initPos;
    private float animDuration;
    private bool animBegin = false;
    private float speed = 4f;

    public void OnDrag(PointerEventData data)
    {
        //transform.Translate(data.delta);
    }

    public void OnEndDrag(PointerEventData data)
    {
        //animBegin = true;
    }

    // Use this for initialization
    void Start () {
        /*initPos = transform.position;
        animDuration = 2f;*/
	}
	
	// Update is called once per frame
	void Update () {
        /*if (animBegin)
        {
            if (animDuration > 0f)
            {
                if (transform.position != initPos)
                {
                    transform.position = Vector3.Lerp(gameObject.transform.position, initPos, Time.deltaTime * speed);
                }
                animDuration -= Time.deltaTime;
            }
            else
            {
                animDuration = 2f;
                animBegin = false;
            }
        }*/
    }
}
