using Mono.Data.Sqlite;

public class Description {
    private int idDescription;
    private string debutPeriodePlantage;
    private string finPeriodePlantage;
    private string debutPeriodeRecolte;
    private string finPeriodeRecolte;
    private int tempsArrossage;
    private string texteDescription;
    private int tempsPousseMin;
    private int tempsPousseMax;

    public Description(SqliteDataReader reader)
    {
        this.idDescription = reader.GetInt32(0);
        this.debutPeriodePlantage = reader.GetString(1);
        this.finPeriodePlantage = reader.GetString(2);
        this.debutPeriodeRecolte = reader.GetString(3);
        this.finPeriodeRecolte = reader.GetString(4);
        this.tempsArrossage = reader.GetInt32(5);
        this.texteDescription = reader.GetString(6);
        this.tempsPousseMin = reader.GetInt32(7);
        this.tempsPousseMax = reader.GetInt32(8);
    }

    // ------------ GETTERS AND SETTERS ---------------

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

    public string DebutPeriodePlantage
    {
        get
        {
            return debutPeriodePlantage;
        }

        set
        {
            debutPeriodePlantage = value;
        }
    }

    public string FinPeriodePlantage
    {
        get
        {
            return finPeriodePlantage;
        }

        set
        {
            finPeriodePlantage = value;
        }
    }

    public string DebutPeriodeRecolte
    {
        get
        {
            return debutPeriodeRecolte;
        }

        set
        {
            debutPeriodeRecolte = value;
        }
    }

    public string FinPeriodeRecolte
    {
        get
        {
            return finPeriodeRecolte;
        }

        set
        {
            finPeriodeRecolte = value;
        }
    }

    public int TempsArrossage
    {
        get
        {
            return tempsArrossage;
        }

        set
        {
            tempsArrossage = value;
        }
    }

    public string TexteDescription
    {
        get
        {
            return texteDescription;
        }

        set
        {
            texteDescription = value;
        }
    }

    public int TempsPousseMin
    {
        get
        {
            return tempsPousseMin;
        }

        set
        {
            tempsPousseMin = value;
        }
    }

    public int TempsPousseMax
    {
        get
        {
            return tempsPousseMax;
        }

        set
        {
            tempsPousseMax = value;
        }
    }
}