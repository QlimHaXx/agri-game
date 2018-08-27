using Mono.Data.Sqlite;

public class Profil {
	private int idProfil;
	private string nom;
    private int idNiveau;
	//private Niveau niveau;
	private int experience;
	private string dateSortie;
    private int tempsPasse;

    public Profil(int idProfil, string nom, int experience, string dateSortie, int tempsPasse, int idNiveau)
    {
        this.idProfil = idProfil;
        this.nom = nom;
        this.experience = experience;
        this.dateSortie = dateSortie;
        this.tempsPasse = tempsPasse;
        this.idNiveau = idNiveau;
    }

    public Profil(SqliteDataReader reader)
    {
        this.idProfil = reader.GetInt32(0);
        this.nom = reader.GetString(1);
        this.experience = reader.GetInt32(3);
        this.dateSortie = reader.GetString(4);
        this.tempsPasse = reader.GetInt32(5);
        this.idNiveau = reader.GetInt32(2);
    }

    // --------- GETTERS ANS SETTERS ---------

    public int IdProfil
    {
        get
        {
            return idProfil;
        }

        set
        {
            idProfil = value;
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

    public int Experience
    {
        get
        {
            return experience;
        }

        set
        {
            experience = value;
        }
    }

    public string DateSortie
    {
        get
        {
            return dateSortie;
        }

        set
        {
            dateSortie = value;
        }
    }

    public int TempsPasse
    {
        get
        {
            return tempsPasse;
        }

        set
        {
            tempsPasse = value;
        }
    }

    public int IdNiveau
    {
        get
        {
            return idNiveau;
        }

        set
        {
            idNiveau = value;
        }
    }
}