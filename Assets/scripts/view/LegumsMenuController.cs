using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegumsMenuController : MonoBehaviour {
	private int idLegume;
	private string nom;
	private string nomImage;
	private Description desc;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int IdLegume {
		get {
			return this.idLegume;
		}
		set {
			idLegume = value;
		}
	}

	public string Nom {
		get {
			return this.nom;
		}
		set {
			nom = value;
		}
	}

	public string NomImage {
		get {
			return this.nomImage;
		}
		set {
			nomImage = value;
		}
	}

	public Description Desc {
		get {
			return this.desc;
		}
		set {
			desc = value;
		}
	}
}
