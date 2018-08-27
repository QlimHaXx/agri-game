using System.Collections.Generic;
using UnityEngine;

public class Jeu : MonoBehaviour
{
    DataBaseConnect dataBase = new DataBaseConnect();
    private List<Description> descriptions;
    private List<Legume> legumes;
    private List<Outil> outils;
    private List<Niveau> niveaux;
    private List<Potager> potager;
    private Profil profil;
    private List<Recolte> recoltes;

    void Awake ()
    {
        this.legumes = dataBase.GetModel<Legume>();
        this.descriptions = dataBase.GetModel<Description>();
        this.outils = dataBase.GetModel<Outil>();
        this.niveaux = dataBase.GetModel<Niveau>();
        this.potager = dataBase.GetModel<Potager>();
        this.recoltes = dataBase.GetModel<Recolte>();
        this.profil = dataBase.GetModel<Profil>()[0];
    }

    void Update() { }

    // GET THE ELEMENT WITH ID
    public Legume GetLegume(int idLegume)
    {
        for (int i = 0; i < legumes.Count; i++)
        {
            if (legumes[i].IdLegume == idLegume)
                return legumes[i];
        }

        return null;
    }

    public Outil GetOutil(int idOutil)
    {
        for (int i = 0; i < outils.Count; i++)
        {
            if (this.outils[i].IdOutils == idOutil)
                return outils[i];
        }

        return null;
    }

    public Niveau GetNiveau(int idNiveau)
    {
        for (int i = 0; i < niveaux.Count; i++)
        {
            if (niveaux[i].IdNiveau == idNiveau)
                return niveaux[i];
        }

        return null;
    }

    public Description GetDescription(int idDescription)
    {
        for (int i = 0; i < descriptions.Count; i++)
        {
            if (descriptions[i].IdDescription == idDescription)
                return descriptions[i];
        }

        return null;
    }

    public Potager GetPotager(int idPotager)
    {
        for (int i = 0; i < potager.Count; i++)
        {
            if (potager[i].IdPotager == idPotager)
                return potager[i];
        }

        return null;
    }

    public Profil GetProfil(int idProfil)
    {
        if (profil.IdProfil == idProfil)
            return profil;

        return null;
    }

    public Recolte GetRecolte(int idRecolte)
    {
        for (int i = 0; i < recoltes.Count; i++)
        {
            if (recoltes[i].IdRecolte == idRecolte)
                return recoltes[i];
        }

        return null;
    }

    // GETTERS AND SETTERS
    public List<Description> Descriptions
    {
        get
        {
            return descriptions;
        }

        set
        {
            descriptions = value;
        }
    }

    public List<Legume> Legumes
    {
        get
        {
            return legumes;
        }

        set
        {
            legumes = value;
        }
    }

    public List<Outil> Outils
    {
        get
        {
            return outils;
        }

        set
        {
            outils = value;
        }
    }

    public List<Niveau> Niveaux
    {
        get
        {
            return niveaux;
        }

        set
        {
            niveaux = value;
        }
    }

    public List<Potager> Potager
    {
        get
        {
            return potager;
        }

        set
        {
            potager = value;
        }
    }

    public Profil Profil
    {
        get
        {
            return profil;
        }

        set
        {
            profil = value;
        }
    }

    public List<Recolte> Recoltes
    {
        get
        {
            return recoltes;
        }

        set
        {
            recoltes = value;
        }
    }
}
