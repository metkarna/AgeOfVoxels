using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvanpostTower : Tower
{
    private UnitSpawner UnitSwown;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        base.TowerPosition = gameObject.transform.position;
        UnitSwown = GameObject.FindObjectOfType(typeof(UnitSpawner)) as UnitSpawner;
        if (base.PlayerColor == "red")
        {
            UnitSwown.spawnPointRed = new Vector3(TowerPosition.x + 20f, TowerPosition.y, TowerPosition.z);
        }
        else{
            UnitSwown.spawnPointBlue = new Vector3(TowerPosition.x + 20f, TowerPosition.y, TowerPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
