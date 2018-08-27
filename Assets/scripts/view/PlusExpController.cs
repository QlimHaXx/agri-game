using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlusExpController : MonoBehaviour {
    private Camera cameraMain;
    private float life;
    private int value;
    private bool adding;
    // Use this for initialization
    void Start()
    {
        cameraMain = Camera.main;
        life = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;

        Vector3 targetPos = new Vector3(cameraMain.transform.localPosition.x - 7f, cameraMain.transform.localPosition.y + 4.4f, 0f);

        if (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * 1f);
        }

        if (life < 0f)
        {
            if (GameObject.Find("UI") != null && adding)
            {
                GameObject.Find("UI").GetComponent<UIManager>().AddScore(value);
            }
            if (GameObject.Find("UI") != null && !adding)
            {
                GameObject.Find("UI").GetComponent<UIManager>().AddScore(-value);
            }
            Destroy(gameObject);
        }
    }

    public void SetXp(int val)
    {
        adding = true;
        value = val;
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "+" + value;
    }

    public void SetMoinsXp(int val)
    {
        adding = false;
        value = val;
        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "-" + value;
    }
}
