using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityNpgsql;

public class DbManager : MonoBehaviour
{
    #region дані для конекту

    public string ipAdress;
    public string port;
    public string login;
    public string password;
    public string dbName;

    #endregion

    private NpgsqlConnection mysql;

    private void Start()
    {
        string connectionStr = string.Format("Host={0};Port={1};Username={2};Password={3};Database={4}",
                                                    ipAdress, port, login, password, dbName);

        mysql = new NpgsqlConnection(connectionStr);

        try
        {
            mysql.Open();
        }
        catch (NpgsqlException ex)
        {
            Debug.LogError(string.Format("ERROR CONNECT: {0}", ex.Message));
        }
    }

    public NpgsqlDataReader query(string sql)
    {
        NpgsqlCommand command = this.mysql.CreateCommand();
        command.CommandText = sql;

        return command.ExecuteReader();
    }
}
