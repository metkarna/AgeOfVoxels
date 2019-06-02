using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvanpostTower : Tower
{
    public GameObject Unit;
    public float coolDown;

    private float AttackTimer = 0;
    private GameObject CurrentUnit;
    private Melee CurrentUserStat;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        base.TowerPosition = gameObject.transform.position;
        CurrentUnit = Instantiate(Unit, new Vector3(TowerPosition.x + 20, TowerPosition.y, TowerPosition.z), new Quaternion(0, 0, 0, 0));
        CurrentUserStat = CurrentUnit.GetComponent(typeof(Melee)) as Melee;
        CurrentUserStat.speed = 0f;
        AttackTimer = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentUnit == null)
        {
            if (AttackTimer > 0)
                AttackTimer -= Time.deltaTime;

            if (AttackTimer < 0)
                AttackTimer = 0;

            if (AttackTimer == 0)
            {
                CurrentUnit = Instantiate(Unit, new Vector3(TowerPosition.x + 20, TowerPosition.y, TowerPosition.z), new Quaternion(0, 0, 0, 0));
                CurrentUserStat = CurrentUnit.GetComponent(typeof(Melee)) as Melee;
                CurrentUserStat.speed = 0f;
                AttackTimer = coolDown;
            }
        }
    }
}
