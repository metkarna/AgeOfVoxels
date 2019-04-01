using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    
    public GameObject tower;
    
    public void BuildTower(GameObject plant)
    {
        plant.SetActive(false);
        Instantiate(tower, plant.transform.position, plant.transform.rotation);
    }
    
}


