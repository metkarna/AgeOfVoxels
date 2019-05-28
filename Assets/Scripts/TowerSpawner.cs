using PlayerIOClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{

    public GameObject battleTower;
    public GameObject baracsTower;
    public GameObject avanpostTower;

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
        player.pioconnection.Send("tsTowerCreate", typeOfUnit, towerIndex, plant.transform.position.x, plant.transform.position.y, plant.transform.position.z, plant.transform.rotation.y);
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
                    switch (m.GetString(0))
                    {
                        case "antiqueBaracsTowerButton":
                            Debug.Log("Барачная башня");
                            Instantiate(baracsTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, m.GetFloat(5), 0, 0));
                            break;
                        case "antiqueBattleTowerButton":
                            Debug.Log("Боевая башня");
                            Instantiate(battleTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, m.GetFloat(5), 0, 0));
                            break;
                        case "antiqueAvanpostTowerButton":
                            Debug.Log("Аванпостная башня");
                            Instantiate(avanpostTower, new Vector3(m.GetFloat(2), m.GetFloat(3), m.GetFloat(4)), new Quaternion(0, m.GetFloat(5), 0, 0));
                            break;

                    }
                    break;
            }
        }
        //player.msgList.Clear();
    }
}


