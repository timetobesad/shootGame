using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityNpgsql;

public class SignManager : MonoBehaviour
{
    public DbManager manager;

    public Text errorBlock;

    public void signIn()
    {
        string login = GameObject.Find("emailField").GetComponent<InputField>().text;
        string pass = GameObject.Find("passField").GetComponent<InputField>().text;

        string sql = string.Format("SELECT uname  FROM users WHERE email='{0}' AND pass='{1}'", login, pass);

        NpgsqlDataReader reader = manager.query(sql);


        bool response = false;

        //Debug.Log(reader["uname"]);
        //PlayerPrefs.SetString("uname", );

        try
        {
            reader.Read();

            Debug.Log(reader[0]);
            PlayerPrefs.SetString("uname", reader[0].ToString());

            response = true;
        }
        catch
        {
             setError("Логин или пароль не верный");
        }

        reader.Close();

        if (response == true)
            SceneManager.LoadScene(1);
           
    }

    private void setError(string errorMsg)
    {
        errorBlock.text = errorMsg;

        resetLoginForm();
    }

    private void resetLoginForm()
    {
        GameObject.Find("emailField").GetComponent<InputField>().text = string.Empty;
        GameObject.Find("passField").GetComponent<InputField>().text = string.Empty;
    }
}
