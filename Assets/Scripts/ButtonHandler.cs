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
    // имя нажатой кнопки
    private string _eventData;

    // хранится ссылка на метод который должен будет выполнен при нажатии
    private delegate void callMe();
    private callMe callAction;
    #endregion

    #region PlantClickCalledMethods
    // запланировать выполнение метода buildTower при нажатии на кнопку
    public void BuildTowerClick() => callAction = BuildTower;

    // Вызвать постройку башни и выключенить меню
    public void BuildTower()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        ui.isFrozen = false;
        GameObject plant = ui.towerPlant;
        var towerSpawner = GameObject.FindObjectOfType(typeof(TowerSpawner)) as TowerSpawner;
        towerSpawner.BuildTower(plant);
        ui.buildMenu.SetActive(false);
        Debug.Log("tower tryed build ");
    }
    #endregion

    #region OpenBuyUnitsClickCalledMethods
    // запланировать выполнение метода CreateUnit при нажатии на кнопку
    public void BuyUnitClick() => callAction = CreateUnit;
    // запланировать выполнение метода OpenBuyUnitsGame при нажатии на кнопку
    public void OpenBuyUnitsMenuClick() => callAction = OpenBuyUnitsGame;
    // запланировать выполнение метода CloseBuyUnitsGame при нажатии на кнопку
    public void CloseBuyUnitsMenuClick() => callAction = CloseBuyUnitsGame;
    // Открыть меню покупки
    private void OpenBuyUnitsGame()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        //ui.isFrozen = true;
        ui.buyUnitsMenu.SetActive(true);
    }
    // Закрыть меню покупки
    private void CloseBuyUnitsGame()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        //ui.isFrozen = false;
        ui.buyUnitsMenu.SetActive(false);
    }

    // Вызвать создание юнита
    private void CreateUnit()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        //ui.isFrozen = false;
        var goldController = GameObject.FindObjectOfType(typeof(GoldConroller)) as GoldConroller;
        if (goldController.BuyUnit(_eventData))
        {
            var unitSpawner = GameObject.FindObjectOfType(typeof(UnitSpawner)) as UnitSpawner;
            unitSpawner.SpawnUnits(_eventData);
        }
    }
    #endregion

    #region PauseGameClickCalledMethods
    // запланировать выполнение метода OpenPauseGame при нажатии на кнопку
    public void OpenPauseMenuClick() => callAction = OpenPauseGame;
    // запланировать выполнение метода ClosePauseGame при нажатии на кнопку
    public void ClosePauseMenuClick() => callAction = ClosePauseGame;
    // Открыть меню паузы
    private void OpenPauseGame()
    {
        Time.timeScale = 0;
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        ui.isFrozen = true;
        ui.pauseGameMenu.SetActive(true);
    }
    // Закрыть меню паузы
    private void ClosePauseGame()
    {
        Time.timeScale = 1;
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        ui.isFrozen = false;
        ui.pauseGameMenu.SetActive(false);
    }
    #endregion

    #region MenuClickCalledMethods
    // запланировать выполнение метода OpenMenu при нажатии на кнопку
    public void OpenMenuClick() => callAction = OpenMenu;
    // Открыть меню уровней( или чего там у нас??????)
    private void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 0;
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        ui.isFrozen = true;
        ui.pauseGameMenu.SetActive(true);
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
