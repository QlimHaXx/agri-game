using Mono.Data.Sqlite;

public class Potager {
    private DataBaseConnect db;    
    private int idPotager;
    private float positionX;
    private float positionY;
    private int idLegume;
    private string datePlantage;
    private int idOutil;
    private string dateDernierArrosage;
    private string tempsRestant;
    private int idProfil;

    public Potager(int idPotager, int idLegume, string datePlantage) // pour maj
    {
        this.idPotager = idPotager;
        this.idLegume = idLegume;
        this.datePlantage = datePlantage;
    }

    public Potager(SqliteDataReader reader)
    {
        this.idPotager = reader.GetInt32(0);
        this.positionX = reader.GetFloat(1);
        this.positionY = reader.GetFloat(2);
        this.datePlantage = (!reader.IsDBNull(4)) ? reader.GetString(4) : "";
        this.dateDernierArrosage = (!reader.IsDBNull(6)) ? reader.GetString(6) : "";
        this.idLegume = reader.GetInt32(3);
        this.idOutil = (!reader.IsDBNull(5)) ? reader.GetInt32(5) : 0;
        this.idProfil = reader.GetInt32(7);
    }

    public int IdPotager
    {
        get
        {
            return idPotager;
        }

        set
        {
            idPotager = value;
        }
    }

    public float PositionX
    {
        get
        {
            return positionX;
        }

        set
        {
            positionX = value;
        }
    }

    public float PositionY
    {
        get
        {
            return positionY;
        }

        set
        {
            positionY = value;
        }
    }

    public string DatePlantage
    {
        get
        {
            return datePlantage;
        }

        set
        {
            datePlantage = value;
        }
    }

    public string DateDernierArrosage
    {
        get
        {
            return dateDernierArrosage;
        }

        set
        {
            dateDernierArrosage = value;
        }
    }

    public string TempsRestant
    {
        get
        {
            return tempsRestant;
        }

        set
        {
            tempsRestant = value;
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

    public int IdOutil
    {
        get
        {
            return idOutil;
        }

        set
        {
            idOutil = value;
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
}
