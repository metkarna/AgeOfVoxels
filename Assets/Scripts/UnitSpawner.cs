using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    #region Fields
    // Точка появления
    // Сделал паблик для тестов
    public Vector3 spawnPointRed = new Vector3(-20, 0.82f, 48);
    public Vector3 spawnPointBlue = new Vector3(-20, 0.82f, 48);
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
    #endregion


    public void SpawnUnitsRed(string typeOfUnit)
    {
        // Название нажатой кнопки вызывает создание соответствующиего юнита
        switch (typeOfUnit)
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

    public void SpawnUnitsBlue(string typeOfUnit)
    {
        // Название нажатой кнопки вызывает создание соответствующиего юнита
        switch (typeOfUnit)
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

}
