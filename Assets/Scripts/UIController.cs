using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buildMenu;
    //private bool builtActive = false;
    public GameObject towerPlant;
    public GameObject buyUnitsMenuRed;
    public GameObject buyUnitsMenuBlue;
    public GameObject pauseGameMenu;
    public GameObject endGameMenu;
    public GameObject goldUI;
    public GameObject redCastleHPUI;
    public GameObject blueCastleHPUI;

    public GameObject btnRed;
    public GameObject btnBlue;

    private Text goldValue;
    private Text redCastleHPValue;
    private Text blueCastleHPValue;
    public bool isFrozen = false;


    private Player player;

    private void Start()
    {
        goldValue = goldUI.GetComponent<Text>();
        redCastleHPValue = redCastleHPUI.GetComponent<Text>();
        blueCastleHPValue = blueCastleHPUI.GetComponent<Text>();
        SetUnitsPrice();
        Debug.Log("value is inited");
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
    }

    private void SetUnitsPrice()
    {
       
    }

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
            }
        }
        RenewPlayerGoldText();
    }

    private void RenewPlayerGoldText()
    {
        goldValue.text = "Золото: " + player.Gold.ToString();
    }

    public void RenewCastleHPText(string color, int hp)
    {
        switch (color)
        {
            case "red":
                redCastleHPValue.text = "Красный: " + hp.ToString();
                break; 
            case "blue":
                blueCastleHPValue.text = "Синий: " + hp.ToString();
                break;  
        }      
    }

    // Если замок уничтожен, вызываем окошко конца игры.
    // Передаем нужное сообщение (победа или поражение)
    public void CastleDestroy(string msg)
    {
        Time.timeScale = 0;
        isFrozen = true;
        Debug.Log(msg);
        endGameMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("endGameMessage").GetComponent<Text>().text = msg;
        Debug.Log("endGameMenu");
    }

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
