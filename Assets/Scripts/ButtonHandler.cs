using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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

    #region CastleClickCalledMethods
    // запланировать выполнение метода CreateUnit при нажатии на кнопку
    public void CastleMenuClick() => callAction = CreateUnit;

    // Вызвать создание юнита и выключить меню
    private void CreateUnit()
    {
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        ui.isFrozen = false;
        ui.buyUnitsMenu.SetActive(false);
        var unitSpawner = GameObject.FindObjectOfType(typeof(UnitSpawner)) as UnitSpawner;
        unitSpawner.SpawnUnits(_eventData); 
    }
    #endregion

    #region EndGameClickCalledMethods
    // запланировать выполнение метода EndGame при нажатии на кнопку
    public void EndGameClick() => callAction = EndGame;

    // Закрыть меню паузы
    private void EndGame()
    {
        Time.timeScale = 1; 
        var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
        ui.isFrozen = false;
        ui.endGameMenu.SetActive(false);
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
