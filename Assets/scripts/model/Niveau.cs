using Mono.Data.Sqlite;

public class Niveau {
	private int idNiveau;
	private string nom;
	private int nbExperience;


    public Niveau(SqliteDataReader reader)
    {
        this.idNiveau = reader.GetInt32(0);
        this.nom = reader.GetString(1);
        this.nbExperience = reader.GetInt32(2);
    }
    
    // -------------- GETTERS AND SETTERS --------------
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