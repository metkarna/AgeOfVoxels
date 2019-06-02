using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarackTower : Tower
{
    private UnitSpawner UnitSwawn;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        base.TowerPosition = gameObject.transform.position;
        UnitSwawn = GameObject.FindObjectOfType(typeof(UnitSpawner)) as UnitSpawner;
        if (base.EnemyTag == "redUnit")
        {
            UnitSwawn.spawnPointBlue = new Vector3(TowerPosition.x + 20f, TowerPosition.y, TowerPosition.z);
        }
        else
        {
            UnitSwawn.spawnPointRed = new Vector3(TowerPosition.x + 20f, TowerPosition.y, TowerPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
