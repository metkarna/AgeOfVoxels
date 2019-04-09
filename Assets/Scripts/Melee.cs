using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee : Unit
{

    private NavMeshAgent navMesh;
    public string EnemyTag;
    private GameObject enemy_castle;
    private GoldConroller goldConroller;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        enemy_castle = GameObject.FindGameObjectWithTag(EnemyTag);
        navMesh = GetComponent<NavMeshAgent>();
        goldConroller = FindObjectOfType(typeof(GoldConroller)) as GoldConroller; 
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_castle != null)
        {
            if (!InBattle)
            {
                navMesh.SetDestination(enemy_castle.transform.position);
            }
        }

        if (_gameObject != null)
        {
            if (Vector3.Distance(transform.position, _gameObject.transform.position) < seeDistance)
            {
                InBattle = true;
                if (Vector3.Distance(transform.position, _gameObject.transform.position) > attackDistance)
                {
                    navMesh.isStopped = false;
                    navMesh.SetDestination(_gameObject.transform.position);
                    _anim.SetBool("Walk", true);
                }
                else
                {
                    navMesh.isStopped = true;
                    if (AttackTimer > 0)
                        AttackTimer -= Time.deltaTime;

                    if (AttackTimer < 0)
                        AttackTimer = 0;

                    if (AttackTimer == 0)
                    {
                        Attack();
                    }
                }
            }
            else
            {
                InBattle = false;
            }
        }
        else
        {
            InBattle = false;
            navMesh.isStopped = false;
            distance = 1000f;
            _anim.SetBool("Hit", false);
            _anim.SetBool("Walk", true);
            _Objects = GameObject.FindGameObjectsWithTag(_tag);
            if (_Objects != null || _Objects.Length > 0)
            {
                foreach (var item in _Objects)
                {

                    if (distance > Vector3.Distance(transform.position, item.transform.position))
                    {
                        distance = Vector3.Distance(transform.position, item.transform.position);
                        _gameObject = item;
                        target = item.transform;
                    }
                }
            }
        }

    }

    private void DealDamage(object[] _data)
    {
        if (health > 0)
        {
            health -= (int)_data[1];
        }
        else
        {
            Destroy(gameObject);
            if ((string)data[0] == "hero")
            {
                goldConroller.KillTrophy(UnitName);
            }
            
        }
    }

    private void Attack()
    {
        _anim.SetBool("Hit", true);
        _gameObject.SendMessage("DealDamage", data);
        AttackTimer = coolDown;
    }
}
