using Mono.Data.Sqlite;

public class Legume {
	private int idLegume;
	private string nom;
    private string nomImage;
    private int idDescription;
    //private Description desc;

    public Legume(SqliteDataReader reader)
    {
        this.idLegume = reader.GetInt32(0);
        this.nom = reader.GetString(1);
        this.nomImage = reader.GetString(2);
        if (!reader.IsDBNull(3))
        {
            this.idDescription = reader.GetInt32(3);
        }
        else
        {
            this.idDescription = -1;
        }
    }
    
    // ----- GETTERS AND SETTERS ------
    public int IdLegume
    {
        get
        {
            return idLegume;
        }

        set
        {
            idLegume = value;
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

    public int IdDescription
    {
        get
        {
            return idDescription;
        }

        set
        {
            idDescription = value;
        }
    }

    /*public Description Desc
    {
        get
        {
            return desc;
        }

        set
        {
            desc = value;
        }
    }*/

    public string NomImage
    {
        get
        {
            return nomImage;
        }

        set
        {
            nomImage = value;
        }
    }
}