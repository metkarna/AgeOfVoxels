﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    #region Fields
    // Точка появления
    // Сделал паблик для тестов
    public Vector3 spawnPoint = new Vector3(-20, 0.82f, 48);
    // Ссылки на копируемые объекты
    public GameObject lumberjack;
    public GameObject archer;
    public GameObject spearman;
    public GameObject swordsman;
    #endregion


    public void SpawnUnits(string typeOfUnit)
    {
        // Название нажатой кнопки вызывает создание соответствующиего юнита
        switch (typeOfUnit)
        {
            case "antiqueLumberjackButton":
                Debug.Log("лесоруб");
                Instantiate(lumberjack, spawnPoint, new Quaternion(0, 0, 0, 0));
                break;
            case "antiqueArcherButton":
                Debug.Log("лучник");
                Instantiate(archer, spawnPoint, new Quaternion(0, 0, 0, 0));
                break;
            case "antiqueSpearmanButton":
                Debug.Log("копейщик");
                Instantiate(spearman, spawnPoint, new Quaternion(0, 0, 0, 0));
                break;
            case "antiqueSwordsmanButton":
                Debug.Log("мечник");
                Instantiate(swordsman, spawnPoint, new Quaternion(0, 0, 0, 0));
                break;

        }
    }

}
