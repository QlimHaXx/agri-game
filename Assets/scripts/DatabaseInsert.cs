using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DatabaseInsert : MonoBehaviour {
    private string name = "databaseMadness";
    private IDbConnection dbconn;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Connect()
    {
        string conn = "URI=file:" + Application.dataPath + "/" + this.name + ".db"; //Path to database.
        this.dbconn = (IDbConnection)new SqliteConnection(conn);

        if (dbconn == null)
        {
            return false;
        }

        return true;
    }

    public void Disconnect()
    {
        this.dbconn.Close();
        this.dbconn = null;
    }
}
