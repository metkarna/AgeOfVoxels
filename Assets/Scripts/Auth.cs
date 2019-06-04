using PlayerIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Auth : MonoBehaviour
{
    public GameObject pl_name;
    public GameObject pl_mail;
    public GameObject pl_pass;
    public GameObject reg_btn;

    private Text pl_name_text;
    private Text pl_mail_text;
    private Text pl_pass_text;

    private string player_name;
    private string player_mail;
    private string player_pass;

    // Start is called before the first frame update
    void Start()
    {
        GetUserData();
        //pl_name_text = pl_name.GetComponent<Text>();
        //player_name = pl_name_text.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUserData()
    {
        pl_name_text = pl_name.GetComponent<Text>();
        pl_mail_text = pl_mail.GetComponent<Text>();
        pl_pass_text = pl_pass.GetComponent<Text>();
        player_name = pl_name_text.text;
        player_mail = pl_mail_text.text;
        player_pass = pl_pass_text.text;
        PlayerPrefs.SetString("Mail", player_mail);
        PlayerPrefs.SetString("Password", player_pass);
        PlayerPrefs.Save();
        PlayerIO.QuickConnect.SimpleRegister(
            "ageofvoxels-rngqg9j7r06969qdooho7w",
            player_name,
            player_pass,
            player_mail,
            null,
            null,
            null,
            null,
            null,
            delegate(Client client)
            {
                Debug.Log("Success registration");
                SceneManager.LoadScene("Menu");
            },
            delegate(PlayerIORegistrationError error)
            {
                Debug.Log(error.Message);
            }
            );
            
        Debug.Log("Save " + player_name + " - " + player_mail + " - " +  player_pass);

    }

    public void LoginUserData()
    {
        pl_mail_text = pl_mail.GetComponent<Text>();
        pl_pass_text = pl_pass.GetComponent<Text>();
        player_mail = pl_mail_text.text;
        player_pass = pl_pass_text.text;
        PlayerPrefs.SetString("Mail", player_mail);
        PlayerPrefs.SetString("Password", player_pass);
        PlayerPrefs.Save();
        PlayerIO.QuickConnect.SimpleConnect(
            "ageofvoxels-rngqg9j7r06969qdooho7w",
            PlayerPrefs.GetString("Mail"),
            PlayerPrefs.GetString("Password"),
            null,
            delegate (Client client)
            {
                SceneManager.LoadScene("Menu");
            },
            delegate (PlayerIOError error)
            {
                Debug.Log(error);
            }
        );
    }

    public void GetUserData()
    {
        pl_name_text = pl_name.GetComponent<Text>();
        if (PlayerPrefs.HasKey("Mail"))
        {
            PlayerIO.QuickConnect.SimpleConnect(
                "ageofvoxels-rngqg9j7r06969qdooho7w",
                PlayerPrefs.GetString("Mail"),
                PlayerPrefs.GetString("Password"),
                null,
                delegate (Client client)
                {
                    reg_btn.SetActive(false);
                    string name = client.ConnectUserId.Substring(6);
                    player_name = name;
                    pl_name_text.text = player_name;
                    Debug.Log("Load " + player_name);
                },
                delegate (PlayerIOError error)
                {
                    Debug.Log(error);
                }
            );
            
        }
    }

    public void UserExit()
    {
        pl_name_text = pl_name.GetComponent<Text>();

        PlayerIO.QuickConnect.SimpleConnect(
            "ageofvoxels-rngqg9j7r06969qdooho7w",
            PlayerPrefs.GetString("Mail"),
            PlayerPrefs.GetString("Password"),
            null,
            delegate (Client client)
            {
                client.Logout();
            },
            delegate (PlayerIOError error)
            {
                Debug.Log(error);
            }
        );

        PlayerPrefs.DeleteKey("Name");
        PlayerPrefs.DeleteKey("Mail");
        PlayerPrefs.DeleteKey("Password");
        PlayerPrefs.Save();

        pl_name_text.text = string.Empty;

        reg_btn.SetActive(true);
    }
}
