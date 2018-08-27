public class Outils {
	private int idOutils;
	private string nom;
	private int nbExperience;

    public Outils(int idOutils, string nom, int nbExperience)
    {
        this.idOutils = idOutils;
        this.nom = nom;
        this.nbExperience = nbExperience;
    }

    public int IdOutils
    {
        get
        {
            return idOutils;
        }

        set
        {
            idOutils = value;
        }
    }

    public string Nom
    {
        get
        {
            return nom;
        }

        set
        {
            nom = value;
        }
    }

    public int NbExperience
    {
        get
        {
            return nbExperience;
        }

        set
        {
            nbExperience = value;
        }
    }
}

