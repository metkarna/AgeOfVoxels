using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee : Unit
{

    private NavMeshAgent navMesh;
    public string EnemyCastleTag;
    private GameObject enemy_castle;
    private GoldConroller goldConroller;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        enemy_castle = GameObject.FindGameObjectWithTag(EnemyCastleTag);
        navMesh = GetComponent<NavMeshAgent>();
        goldConroller = FindObjectOfType(typeof(GoldConroller)) as GoldConroller;
        if (!InBattle)
        {
            navMesh.SetDestination(enemy_castle.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_castle != null)
        {
            if (!InBattle)
            {
                /*if (navMesh.isActiveAndEnabled)
                {
                    if (navMesh.isStopped)
                    {
                        navMesh.isStopped = false;
                    }
                }*/
                navMesh.speed = 3.5f;
            }
        }

        if (_gameObject != null)
        {
            if (Vector3.Distance(transform.position, _gameObject.transform.position) < seeDistance)
            {
                InBattle = true;
                if (Vector3.Distance(transform.position, _gameObject.transform.position) > attackDistance)
                {
                    //transform.Translate(new Vector3(0, 0, 0));
                    _anim.SetBool("Walk", true);
                    navMesh.speed = 3.5f;
                }
                else
                {
                    _anim.SetBool("Walk", false);
                    _anim.SetBool("Hit", true);
                    transform.LookAt(_gameObject.transform);
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
                /*if (navMesh.isActiveAndEnabled)
                {
                    if (navMesh.isStopped)
                    {
                        navMesh.isStopped = false;
                    }
                }*/
                navMesh.speed = 3.5f;
            }
        }
        else
        {
            InBattle = false;
            distance = 1000f;
            _anim.SetBool("Hit", false);
            _anim.SetBool("Walk", true);
            _Objects = GameObject.FindGameObjectsWithTag(enemyTag);
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
            //Destroy(gameObject);
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            _anim.Play("Death");
             Destroy (gameObject, this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            if ((string)data[0] == "hero")
            {
                goldConroller.KillTrophy(UnitName);
            }
            
        }
    }

    private void Attack()
    {
        
        _gameObject.SendMessage("DealDamage", data);
        AttackTimer = coolDown;
        navMesh.speed = 0;
        /*if (navMesh.isActiveAndEnabled)
        {
            if (!navMesh.isStopped)
            {
                navMesh.isStopped = true;
            }
        }*/
    }
}
