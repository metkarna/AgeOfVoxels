using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerIOClient;

public class UnitSpawner : MonoBehaviour
{
    #region Fields
    // Точка появления
    // Сделал паблик для тестов
    public Vector3 spawnPointRed = new Vector3(-20, 0.82f, 48);
    public Vector3 spawnPointBlue = new Vector3(-20, 0.82f, -48);
    // Ссылки на копируемые объекты для игрока
    public GameObject lumberjackRed;
    public GameObject archerRed;
    public GameObject spearmanRed;
    public GameObject swordsmanRed;

    // Ссылки на копируемые объекты для противника
    public GameObject lumberjackBlue;
    public GameObject archerBlue;
    public GameObject spearmanBlue;
    public GameObject swordsmanBlue;

    // Игроки
    private Player player;
    private UIController ui;
    #endregion

    public void Start(){
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
    }

    public void SpawnUnitsRed(string typeOfUnit)
    {
        player.pioconnection.Send("tsUnitCreate", typeOfUnit, "red");
    }

    public void SpawnUnitsBlue(string typeOfUnit)
    {
        player.pioconnection.Send("tsUnitCreate", typeOfUnit, "blue");
    }

    void FixedUpdate() {
        // process message queue
        foreach (Message m in player.msgList) {
            switch (m.Type) {
                case "fsUserColor":
                    player.UserColor = m.GetString(0);
                    ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
                    if (player.UserColor == "red")
                    {
                        ui.btnRed.SetActive(true);
                    }
                    else
                    {
                        ui.btnBlue.SetActive(true);
                    }

                    Debug.Log("User color is " + player.UserColor);
                    break;
                case "fsUnitCreate":
                    if(m.GetString(1) == "red")
                    {
                        switch (m.GetString(0))
                        {
                            case "antiqueLumberjackButton":
                                Debug.Log("лесоруб");
                                Instantiate(lumberjackRed, spawnPointRed, new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueArcherButton":
                                Debug.Log("лучник");
                                Instantiate(archerRed, spawnPointRed, new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueSpearmanButton":
                                Debug.Log("копейщик");
                                Instantiate(spearmanRed, spawnPointRed, new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueSwordsmanButton":
                                Debug.Log("мечник");
                                Instantiate(swordsmanRed, spawnPointRed, new Quaternion(0, 0, 0, 0));
                                break;
                        }
                    }
                    else
                    {
                        switch (m.GetString(0))
                        {
                            case "antiqueLumberjackButton":
                                Debug.Log("лесоруб");
                                Instantiate(lumberjackBlue, spawnPointBlue, new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueArcherButton":
                                Debug.Log("лучник");
                                Instantiate(archerBlue, spawnPointBlue, new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueSpearmanButton":
                                Debug.Log("копейщик");
                                Instantiate(spearmanBlue, spawnPointBlue, new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueSwordsmanButton":
                                Debug.Log("мечник");
                                Instantiate(swordsmanBlue, spawnPointBlue, new Quaternion(0, 0, 0, 0));
                                break;
                        }
                    }
                    break;
            }
        }
        player.msgList.Clear();
    }
}
