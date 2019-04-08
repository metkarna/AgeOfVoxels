using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{

    public GameObject battleTower;
    public GameObject baracsTower;
    public GameObject avanpostTower;

    public void BuildTower(GameObject plant)
    {
      //  plant.SetActive(false);
     //   Instantiate(tower, plant.transform.position, plant.transform.rotation);
    }
    public void SpawnTowerRed(string typeOfUnit, GameObject plant)
    {
        plant.SetActive(false);
        // Название нажатой кнопки вызывает создание соответствующиего юнита
        switch (typeOfUnit)
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

        }
    }

}


