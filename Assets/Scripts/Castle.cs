using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerIOClient;

public class Castle : MonoBehaviour
{
    public string Enemy_Tag;
    public int Castle_Hitpoint;
    private GameObject _enemy;
    private Player player;
    private UIController ui;

     private void Start()
     {
         player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        
     }

    void OnCollisionEnter(Collision collision)
    {
        string msg;
        if (collision.gameObject.tag == Enemy_Tag)
        {
            Destroy(collision.gameObject);
            Castle_Hitpoint--;
            if (Castle_Hitpoint == 0)
            {
                ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
                if (player.UserColor == "red")
                {
                    msg = gameObject.tag == "blueCastle" ? "Победа :)" : "Поражение :(";
                }
                else
                {
                    msg = gameObject.tag == "redCastle" ? "Победа :)" : "Поражение :(";
                }
                ui.CastleDestroy(msg);
                Destroy(gameObject);
            }
            //player.pioconnection.Send("tsCastleHP");
        }
    }

    // void FixedUpdate() {
    //     // process message queue
    //     foreach (Message m in player.msgList) {
    //         switch (m.Type) {
    //             case "fsCastleHP":
    //                 break;
    //         }
    //     }
    //     //player.msgList.Clear();
    // }
}
