using Mono.Data.Sqlite;

public class Outil {
	private int idOutils;
	private string nom;
	private int nbExperience;

    public Outil(SqliteDataReader reader)
    {
        this.idOutils = reader.GetInt32(0);
        this.nom = reader.GetString(1);
        this.nbExperience = reader.GetInt32(2);
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

