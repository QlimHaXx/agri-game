using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class DatabaseReset : MonoBehaviour {
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetDatabase()
    {
        string command = File.ReadAllText(Application.dataPath + "/" + "reset.sql"); // .sql file path

        using (SqliteConnection c = new SqliteConnection(@"Data Source=" + Application.dataPath + "\\databaseMadness.db;Pooling=true;FailIfMissing=false;Version=3"))
        {
            c.Open();
            using (SqliteCommand cmd = new SqliteCommand(command, c))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
