using Mono.Data.Sqlite;

public class Recolte {
	private int idRecolte;
    private int idProfil;
    private int idLegume;
	private int nbRecoltes;
	private int nbMalRecoltes;

    public Recolte(SqliteDataReader reader)
    {
        this.idRecolte = reader.GetInt32(0);
        this.nbRecoltes = reader.GetInt32(3);
        this.nbMalRecoltes = reader.GetInt32(4);
        this.idProfil = reader.GetInt32(1);
        this.idLegume = reader.GetInt32(2);
    }

    public int IdRecolte
    {
        get
        {
            return idRecolte;
        }

        set
        {
            idRecolte = value;
        }
    }

    public int NbRecoltes
    {
        get
        {
            return nbRecoltes;
        }

        set
        {
            nbRecoltes = value;
        }
    }

    public int NbMalRecoltes
    {
        get
        {
            return nbMalRecoltes;
        }

        set
        {
            nbMalRecoltes = value;
        }
    }

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
}