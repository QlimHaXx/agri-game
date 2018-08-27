using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DatabaseUpdate : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateProfil(Profil profil)
    {
        string command = "UPDATE Profil SET nom = @nom, IdNiveau = @idNiveau, Exp = @exp, Date = @date, TempsPasse = @tempsPasse WHERE IdProfl = @id;";
        
        try
        {
            using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
            {
                c.Open();
                using (SqliteCommand cmd = new SqliteCommand(command, c))
                {
                    cmd.Parameters.Add(new SqliteParameter("@id", profil.IdProfil));
                    cmd.Parameters.Add(new SqliteParameter("@nom", profil.Nom));
                    cmd.Parameters.Add(new SqliteParameter("@exp", profil.Experience));
                    cmd.Parameters.Add(new SqliteParameter("@date", profil.DateSortie));
                    cmd.Parameters.Add(new SqliteParameter("@tempsPasse", profil.TempsPasse));
                    cmd.Parameters.Add(new SqliteParameter("@idNiveau", profil.IdNiveau));
                    cmd.ExecuteNonQuery();
                }
            }
            
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void UpdateProfilNiveau(int idProfil, int idNiveau)
    {
        string command = "UPDATE Profil SET IdNiveau = @idNiveau WHERE IdProfl = @id;";

        try
        {
            using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
            {
                c.Open();
                using (SqliteCommand cmd = new SqliteCommand(command, c))
                {
                    cmd.Parameters.Add(new SqliteParameter("@id", idProfil));
                    cmd.Parameters.Add(new SqliteParameter("@idNiveau", idNiveau));
                    cmd.ExecuteNonQuery();
                }
            }

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void UpdatePotager(Potager potager)
    {
        string command = "UPDATE Potager SET IdLegume = @idLegume, DatePlantage = @datePlantage WHERE IdPotager = @id;";
        try
        {
            using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
            {
                c.Open();
                using (SqliteCommand cmd = new SqliteCommand(command, c))
                {
                    cmd.Parameters.Add(new SqliteParameter("@id", potager.IdPotager));
                    cmd.Parameters.Add(new SqliteParameter("@idLegume", potager.IdLegume));
                    cmd.Parameters.Add(new SqliteParameter("@datePlantage", potager.DatePlantage));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void UpdateProfilDate(string date)
    {
        string command = "UPDATE Profil SET Date = @dateSortie WHERE IdProfl = 1;";

        try
        {
            using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
            {
                c.Open();
                using (SqliteCommand cmd = new SqliteCommand(command, c))
                {
                    cmd.Parameters.Add(new SqliteParameter("@dateSortie", date));
                    cmd.ExecuteNonQuery();
                }
            }

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void UpdateProfilExp(int exp)
    {
        string command = "UPDATE Profil SET Exp = @exp WHERE IdProfl = 1;";

        try
        {
            using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
            {
                c.Open();
                using (SqliteCommand cmd = new SqliteCommand(command, c))
                {
                    cmd.Parameters.Add(new SqliteParameter("@exp", exp));
                    cmd.ExecuteNonQuery();
                }
            }

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void UpdateDateArrosage(int idPotager, string dateArrosage)
    {
        string command = "UPDATE Potager SET DateDernierArrosage = @dateArr WHERE IdPotager = @idPotager;";

        try
        {
            using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
            {
                c.Open();
                using (SqliteCommand cmd = new SqliteCommand(command, c))
                {
                    cmd.Parameters.Add(new SqliteParameter("@dateArr", dateArrosage));
                    cmd.Parameters.Add(new SqliteParameter("@idPotager", idPotager));
                    cmd.ExecuteNonQuery();
                }
            }

            UpdateProfilDate(dateArrosage);

        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void UpdateNbLegumes(int idRecolte, int nbLegume, int nbLegumesMal)
    {
        Debug.Log(nbLegume);
        Debug.Log(nbLegumesMal);

        string command = "UPDATE Recolte SET NbRecoltes = @nbRecoltes, NbMalRecoltes = @nbMal WHERE IdRecolte = @idRecolte;";

        try
        {
            using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
            {
                c.Open();
                using (SqliteCommand cmd = new SqliteCommand(command, c))
                {
                    cmd.Parameters.Add(new SqliteParameter("@nbRecoltes", nbLegume));
                    cmd.Parameters.Add(new SqliteParameter("@nbMal", nbLegumesMal));
                    cmd.Parameters.Add(new SqliteParameter("@idRecolte", idRecolte));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}
