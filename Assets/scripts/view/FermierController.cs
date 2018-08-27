using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FermierController : MonoBehaviour {
    private Camera cameraMain;
    private float life;

    private string agrandir = "Vous pouvez agrandir votre potager en cliquant sur";
    private string recolter_int = "Vous pouvez récolter des légumes !"; // que si intermédiaire
    
    private string deblocage = "Tu as débloqué un nouvel objet, vas voir dans ton panier !";
    private string pasRecolter = "Le légume n'est pas encore prêt, attend qu'il soit mûr avant de le récolter.";
    private string manqueEau_int = "Vos légumes manquent d'eau"; // si intermediaire
    private string pasPlanter = "Vous ne pouvez pas planter ce légume maintenant, ce n'est pas la bonne période.";

    private string nonAgrandissable = "Terrain non agrandissable, vous pouvez l'agrandir tous les 2500 points";

    private GameObject ui;

    // Use this for initialization
    void Start () {
        ui = GameObject.FindGameObjectWithTag("UI");
        if (!ui.GetComponent<UIManager>().Aide)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector3(960f, -500f, 0f);
        cameraMain = Camera.main;
        life = 6f;
    }
	
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;

        Vector3 targetPos = new Vector3(960f, 0f, 0f);

        if (transform.position != targetPos)
        {
            transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * 2f);
        }

        if (life < 0f)
        {
            targetPos = new Vector3(960f, -800f, 0f);
            transform.position = Vector3.Lerp(gameObject.transform.position, targetPos, Time.deltaTime * 2f);
        }

        if(life < -1f)
        {
            Destroy(gameObject);
        }
    }

    public string Agrandir
    {
        get
        {
            return agrandir;
        }

        set
        {
            agrandir = value;
        }
    }

    public string Recolter_int
    {
        get
        {
            return recolter_int;
        }

        set
        {
            recolter_int = value;
        }
    }

    

    public string Deblocage
    {
        get
        {
            return deblocage;
        }

        set
        {
            deblocage = value;
        }
    }

    public string PasRecolter
    {
        get
        {
            return pasRecolter;
        }

        set
        {
            pasRecolter = value;
        }
    }

    public string ManqueEau_int
    {
        get
        {
            return manqueEau_int;
        }

        set
        {
            manqueEau_int = value;
        }
    }

    public string PasPlanter
    {
        get
        {
            return pasPlanter;
        }

        set
        {
            pasPlanter = value;
        }
    }

    public void SetPhraseRecolterInt()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = recolter_int;
    }

    public void SetPhraseAgrandir()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = agrandir;
    }

    public void SetPhraseDeblocage()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = deblocage;
    }

    public void SetPhraseManqueEauInt()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = manqueEau_int;
    }

    public void SetPhrasePasPlanter()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = pasPlanter;
    }

    public void SetPhrasePasRecolter()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = pasRecolter;
    }

    public void SetPhraseNonAgrandissable()
    {
        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = nonAgrandissable;
    }
}
