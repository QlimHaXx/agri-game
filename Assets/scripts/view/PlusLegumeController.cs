using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlusLegumeController : MonoBehaviour
{
    private Camera cameraMain;
    private float life;
    private int value;
    // Use this for initialization
    void Start()
    {
        cameraMain = Camera.main;
        life = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;

        Vector3 targetPos = new Vector3(cameraMain.transform.localPosition.x + 8f, cameraMain.transform.localPosition.y + 4.4f, 0f);

        if (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * 2f);
        }

        if (life < 0f)
        {
            Destroy(gameObject);
        }
    }
}
