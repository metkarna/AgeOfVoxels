using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerIOClient;
using System;

public class Player : MonoBehaviour
{
    public int Gold { get; set; }

    private string gameID = "ageofvoxels-rngqg9j7r06969qdooho7w";

    public List<Message> msgList = new List<Message>(); //  Messsage queue implementation
    private bool joinedroom = false;
    public Connection pioconnection;
    private string infomsg = "";


    public int Name { get; set; }
    private const int defaultGoldValue = 300;

    private void Start()
    {
        /*PlayerIO.Authenticate(gameID, "public", new Dictionary<string, string> { { "userId", "TestUser" }, }, null, SuccessCallback, ErrorCallback);

        PlayerIO.Connect(gameID, "public", "user-id", null, null, SuccessCallback, ErrorCallback);*/

        System.Random random = new System.Random();
        string userid = "Guest" + random.Next(0, 10000);

        Debug.Log("Starting");

        PlayerIO.Authenticate(
            gameID,                                 // Your game id
            "public",                               //Your connection id
            new Dictionary<string, string> {        //Authentication arguments
				{ "userId", userid },
            },
            null,                                   //PlayerInsight segments
            delegate (Client client) {
                Debug.Log("Successfully connected to Player.IO");
                infomsg = "Successfully connected to Player.IO";
                

                Debug.Log("Create ServerEndpoint");
                // Comment out the line below to use the live servers instead of your development server
                //client.Multiplayer.DevelopmentServer = new ServerEndpoint("localhost", 8184);

                Debug.Log("CreateJoinRoom");
                //Create or join the room (Создаем и/или сразу входим в комнату)
                client.Multiplayer.CreateJoinRoom(
                    "UnityDemoRoom",                    //Room id. If set to null a random roomid is used
                    "UnityMushrooms",                   //The room type started on the server
                    true,                               //Should the room be visible in the lobby?
                    null,
                    null,
                    delegate (Connection connection) {
                        Debug.Log("Joined Room.");
                        infomsg = "Joined Room.";
                        // We successfully joined a room so set up the message handler
                        pioconnection = connection;
                        pioconnection.OnMessage += handlemessage;
                        joinedroom = true;
                    },
                    delegate (PlayerIOError error) {
                        Debug.Log("Error Joining Room: " + error.ToString());
                        infomsg = error.ToString();
                    }
                );
            },
            delegate (PlayerIOError error) {
                Debug.Log("Error connecting: " + error.ToString());
                infomsg = error.ToString();
            }
        );

        Gold = defaultGoldValue;
    }

    void FixedUpdate() {
        // process message queue
        /*foreach (Message m in msgList) {
            switch (m.Type) {
                case "PlayerJoined":
                    GameObject newplayer = GameObject.Instantiate(target) as GameObject;
                    newplayer.transform.position = new Vector3(m.GetFloat(1), 0, m.GetFloat(2));
                    newplayer.name = m.GetString(0);
                    newplayer.transform.Find("NameTag").GetComponent<TextMesh>().text = m.GetString(0);
                    break;

            }
        }*/
        // clear message queue after it's been processed
        msgList.Clear();
    }

    void handlemessage(object sender, Message m)
    {
        msgList.Add(m);
    }

    private void SuccessCallbackConnection(Connection value)
    {
        Debug.Log("Success");
    }

    private void ErrorCallback(PlayerIOError value)
    {
        Debug.Log("Error connect");
    }

    private void SuccessCallback(Client value)
    {
        Debug.Log("Success");
    }
}
