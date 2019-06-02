using PlayerIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{

    public GameObject redbattleTower;
    public GameObject redbaracsTower;
    public GameObject redavanpostTower;

    public GameObject bluebattleTower;
    public GameObject bluebaracsTower;
    public GameObject blueavanpostTower;

    private Player player;

    private GameObject[] towerPlants;

    public void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        towerPlants = GameObject.FindGameObjectsWithTag("tower-plant");
    }

    public void BuildTower(GameObject plant)
    {
        //  plant.SetActive(false);
        //   Instantiate(tower, plant.transform.position, plant.transform.rotation);
    }
    public void SpawnTowerRed(string typeOfUnit, GameObject plant)
    {
        //plant.SetActive(false);
        int towerIndex = Array.IndexOf(towerPlants, plant);
        player.pioconnection.Send("tsTowerCreate", typeOfUnit, towerIndex, plant.transform.position.x, plant.transform.position.y, plant.transform.position.z, player.UserColor);
        // Название нажатой кнопки вызывает создание соответствующиего юнита
        /*switch (typeOfUnit)
        {
            case "antiqueBaracsTowerButton":
                Debug.Log("Барачная башня");
                Instantiate(baracsTower, plant.transform.position, plant.transform.rotation);
                break;
            case "antiqueBattleTowerButton":
                Debug.Log("Боевая башня");
                Instantiate(battleTower, plant.transform.position, plant.transform.rotation);
                break;
            case "antiqueAvanpostTowerButton":
                Debug.Log("Аванпостная башня");
                Instantiate(avanpostTower, plant.transform.position, plant.transform.rotation);
                break;

        }*/
    }

    void FixedUpdate()
    {
        foreach (Message m in player.msgList)
        {
            switch (m.Type)
            {
                case "fsTowerCreate":
                    towerPlants[m.GetInt(1)].SetActive(false);
                    if (m.GetString(5) == "red")
                    {
                        switch (m.GetString(0))
                        {
                            case "antiqueBaracsTowerButton":
                                Debug.Log("Барачная башня");
                                Instantiate(redbaracsTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueBattleTowerButton":
                                Debug.Log("Боевая башня");
                                Instantiate(redbattleTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueAvanpostTowerButton":
                                Debug.Log("Аванпостная башня");
                                Instantiate(redavanpostTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, 0, 0, 0));
                                break;
                        }
                        break;
                    }
                    else
                    {
                        switch (m.GetString(0))
                        {
                            case "antiqueBaracsTowerButton":
                                Debug.Log("Барачная башня");
                                Instantiate(bluebaracsTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueBattleTowerButton":
                                Debug.Log("Боевая башня");
                                Instantiate(bluebattleTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, 0, 0, 0));
                                break;
                            case "antiqueAvanpostTowerButton":
                                Debug.Log("Аванпостная башня");
                                Instantiate(blueavanpostTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, 0, 0, 0));
                                break;
                        }
                        break;
                    }
                    
            }
        }
        //player.msgList.Clear();
    }
}


