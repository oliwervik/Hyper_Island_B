//Database.cs
using UnityEngine;
using SQLite;
using System;
using System.IO;
using System.Collections.Generic;

public class Database : MonoBehaviour 
{
    // Singleton for easier access
    public static Database singleton;

    // Connection
    SQLiteConnection connection;

    //Location of the database file
    public string databaseFile = "Database.sqlite";


    public class accounts
    {
        [PrimaryKey]
        public string name { get; set; }
        public string email { get; set; }
        public DateTime created { get; set; }
    }

    private void Awake()
    {
        // intialize singleton
        if (singleton == null) singleton = this;

        Connect();
    }

    public void Connect()
    {
        string path = Path.Combine(Directory.GetParent(Application.dataPath).FullName, databaseFile);

        connection = new SQLiteConnection(path);

        connection.CreateTable<accounts>();

        Debug.Log($"Database connected to: {path}");
    }

    public bool AddAccount(string name, string email)
    {
        try
        {
            var account = new accounts { name = name, email = email, created = DateTime.UtcNow };
            connection.Insert(account);
            Debug.Log($"Account added: {name}, {email}, {account.created}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to add account: {ex.Message}");
            return false;
        }
    }

    public accounts GetAccount(string name)
    {
        return connection.Table<accounts>().FirstOrDefault(acc => acc.name == name);
    }

    public List<accounts> GetAllAccounts()
    {
        return connection.Table<accounts>().ToList();
    }

    void OnApplicationQuit()
    {
        connection?.Close();
    }

}