using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DataBaseConnect {
    
    public List<T> GetModel<T>()
    {
        List<T> models = new List<T>();
        string command = "SELECT * FROM " + typeof(T).FullName;
        using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(command, c))
            {
                using (SqliteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        models.Add((T)Activator.CreateInstance(typeof(T), reader));
                    }
                }
            }
        }
        return models;
    }
}