using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldConroller : MonoBehaviour
{

  //  System.Single time;
    public float timeRemaining = 1f;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        // var time = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0) {
                GiveDeltaTimeGold();
                timeRemaining = 1f;
            }
        }
    }
    private void GiveDeltaTimeGold()
    {
        // Дать игроку золото
       player.Gold += 3;
       Debug.Log("Player received 3 gold");
    }
    public bool BuyUnit(string typeOfUnit)
    {
        var cost = 0;
        // <ДУБЛИРОВАНИЕ КОДА ИЗ UNITSPAWNER>
        switch (typeOfUnit)
        {
            case "antiqueLumberjackButton":
                Debug.Log("Куплен лесоруб");
                cost = (int)UnitCosts.Lumberjack;
                break;
            case "antiqueArcherButton":
                Debug.Log("Куплен лучник");
                cost = (int)UnitCosts.Archer;
                break;
            case "antiqueSpearmanButton":
                Debug.Log("Куплен копейщик");
                cost = (int)UnitCosts.Spearman;
                break;
            case "antiqueSwordsmanButton":
                Debug.Log("Куплен мечник");
                cost = (int)UnitCosts.Swordsman;
                break;

        }
        // </ДУБЛИРОВАНИЕ КОДА ИЗ UNITSPAWNER>
        return GoldTransaction(cost);
    }
    private bool GoldTransaction(int cost)
    {
        if (cost <= player.Gold)
        {
            player.Gold -= cost;
            return true;
        }
        else return false;
    }
    private enum UnitCosts { Lumberjack = 30, Archer = 100, Spearman = 50, Swordsman = 70 }
}
