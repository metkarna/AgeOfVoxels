using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buildMenu;
    //private bool builtActive = false;
    public GameObject towerPlant;
    public GameObject buyUnitsMenu;
    public GameObject pauseGameMenu;
    public bool isFrozen = false;

    // Получаем нажатый элемент и проверяем можно ли вызывать меню (!isFrozen)
    void Update()
    {
        if (!isFrozen && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject clickedObject = hit.transform.gameObject;
                if (clickedObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity) &&
                    clickedObject.CompareTag("tower-plant"))
                {
                    TowerPlantClick(hit);
                }
                else if (clickedObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity) &&
                    clickedObject.CompareTag("playerCastle"))
                {
                    PlayerCastleClick();
                }
                // else if (clickedObject.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity) &&
                //     clickedObject.CompareTag("castle"))
                // {
                //     EndGameClick();
                // }
            }
        }
    }
    // Если кликнули на замок - активируем его меню и замораживаем возможность вызова интерфейса
    private void PlayerCastleClick()
    {
        buyUnitsMenu.SetActive(true);
        isFrozen = true;
        Debug.Log("castleMenu");
    }

    // Если кликнули на замок - активируем его меню и замораживаем возможность вызова интерфейса
    // private void PauseGameClick()
    // {
    //     Time.timeScale = 0;
    //     pauseGameMenu.SetActive(true);
    //     isFrozen = true;
    //     Debug.Log("endGameMenu");
    // }

    // Если кликнули на плент - активируем его меню и замораживаем возможность вызова интерфейса
    private void TowerPlantClick(RaycastHit hit)
    {
        // TowerSpawner.BuildTower(hit, ray);
        towerPlant = hit.transform.gameObject;
        buildMenu.SetActive(true);
        isFrozen = true;
        Debug.Log("towerMenu");
    }
    
}
