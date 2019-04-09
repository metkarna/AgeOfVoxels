using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class ButtonHandler : MonoBehaviour, IPointerClickHandler
{
    #region Fields
    // Имя нажатой кнопки
    private string _eventData;
    // Хранится ссылка на метод который должен будет выполнен при нажатии
    private delegate void callMe();
    private callMe callAction;
    #endregion

    #region PlantClickCalledMethods
    // Запланировать выполнение метода buildTower при нажатии на кнопку
    public void BuildTowerClick() => callAction = BuildRedTower;
    // Вызвать постройку башни и выключенить меню
    public void BuildRedTower()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        ui.isFrozen = false;
        
        var goldController = GameObject.FindObjectOfType(typeof(GoldConroller)) as GoldConroller;
        if (goldController.BuyTower(_eventData))
        {
            GameObject plant = ui.towerPlant;
            var towerSpawner = GameObject.FindObjectOfType(typeof(TowerSpawner)) as TowerSpawner;
            towerSpawner.SpawnTowerRed(_eventData, plant);
        }
        ui.buildMenu.SetActive(false);
        Debug.Log("tower tryed build ");
    }
    #endregion

    // Покупка и спавн героев игроком
    #region OpenBuyUnitsRedClickCalledMethods
    // Запланировать выполнение метода CreateUnit при нажатии на кнопку
    public void BuyUnitClickRed() => callAction = CreateUnitRed;
    public void BuyUnitClickBlue() => callAction = CreateUnitBlue;
    // Вызвать создание юнита
    private void CreateUnitRed()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        var unitSpawner = GameObject.FindObjectOfType(typeof(UnitSpawner)) as UnitSpawner;
        var goldController = GameObject.FindObjectOfType(typeof(GoldConroller)) as GoldConroller;
        if (goldController.BuyUnit(_eventData))
        {
            unitSpawner.SpawnUnitsRed(_eventData);
        }
    }
    private void CreateUnitBlue()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        var unitSpawner = GameObject.FindObjectOfType(typeof(UnitSpawner)) as UnitSpawner;
		unitSpawner.SpawnUnitsBlue(_eventData);
    }
    #endregion

    #region MenusCalledMethods
    // Запланировать выполнение метода OpenSceneClick при нажатии на кнопку
    public void OpenMenuClick() => callAction = OpenMenu;
    public void CloseMenuClick() => callAction = CloseMenu;
    // Открыть сцену
    private void OpenMenu()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        // Название нажатой кнопки вызывает открытие соответствующего меню
        switch (_eventData)
        {   
            case "btnOpenPause":
                Time.timeScale = 0;
                ui.isFrozen = true;
                ui.pauseGameMenu.SetActive(true);
                break;
            case "btnOpenBuyUnitsBlue":
                ui.buyUnitsMenuBlue.SetActive(true);
                break;
            case "btnOpenBuyUnitsRed":
                ui.buyUnitsMenuRed.SetActive(true);
                break;
        }
    }
    private void CloseMenu()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        // Название нажатой кнопки вызывает зыкрытие соответствующего меню
        switch (_eventData)
        {
            case "btnClosePause":
                Time.timeScale = 1;
                ui.isFrozen = false;
                ui.pauseGameMenu.SetActive(false);
                break;
            case "btnCloseBuyUnitsBlue":
                ui.buyUnitsMenuBlue.SetActive(false);
                break;
            case "btnCloseBuyUnitsRed":
                ui.buyUnitsMenuRed.SetActive(false);
                break;
            case "btnCloseBuyTowersRed":
                ui.isFrozen = false;
                ui.buildMenu.SetActive(false);
                break;
        }
    }
    #endregion

    #region OpenScenesCalledMethods
    // Запланировать выполнение метода OpenSceneClick при нажатии на кнопку
    public void OpenSceneClick() => callAction = OpenScene;
    // Открыть сцену
    private void OpenScene()
    {
        // Название нажатой кнопки вызывает загрузку соответствующиего сцены
        switch (_eventData)
        {
            case "level-test":
                Debug.Log("Открытие сцены: Тестовый уровень");
                SceneManager.LoadScene("test-map");
                break;
            case "level 1":
                Debug.Log("Открытие сцены: Уровень Map1-1");
                SceneManager.LoadScene("Map1-1");
                break;
            case "btnGoToMenu":
                Debug.Log("Открытие сцены: Меню");
                SceneManager.LoadScene("Menu");
                break;
            case "btnGoToLevels":
                Debug.Log("Открытие сцены: Уровни");
                SceneManager.LoadScene("Levels");
                break;
            case "btnGoToOptions":
                Debug.Log("Открытие сцены: Уровни");
                SceneManager.LoadScene("Options");
                break;
            case "btnExit":
                Debug.Log("Закрытие игры");
                UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();
                break;                
        }
    }
    #endregion

    // Реализация интерфейса IPointerClickHandler, метод получает имя нажатой кнопки
    // и вызывает запланированный метод
    public void OnPointerClick(PointerEventData eventData)
    {
        _eventData = eventData.pointerCurrentRaycast.gameObject.transform.parent.name;
        callAction();
        Debug.Log(_eventData);
    }
}
