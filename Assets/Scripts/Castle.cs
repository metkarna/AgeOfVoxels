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
        ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Enemy_Tag)
        {
            Destroy(collision.gameObject);
            player.pioconnection.Send("tsCastleHP");
        }
    }

    void FixedUpdate() {
        // process message queue
        foreach (Message m in player.msgList) {
            switch (m.Type) {
                case "fsCastleHP":
                    Castle_Hitpoint--;
                    if (Castle_Hitpoint == 0)
                    {
                        string msg = gameObject.tag == "enemyCastle" ? "Победа :)": "Поражение :(";
                        ui.CastleDestroy(msg);
                        Destroy(gameObject);
                    }
                    break;
            }
        }
        //player.msgList.Clear();
    }
}
