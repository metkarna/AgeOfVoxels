using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Distant : Unit
{

    public GameObject arrowPrefab;

    private GameObject _arrow;
    private GameObject[] _Towers;
    private GameObject _tower;
    private float distanceTotower = 1000f;
    private Transform towertarget;

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
                navMesh.speed = speed;
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
                    navMesh.speed = speed;
                }
                else
                {
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
                navMesh.speed = speed;
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
                        InBattle = true;
                    }
                }
                if (_gameObject != null)
                {
                   // transform.LookAt(_gameObject.transform);
                }
            }
        }

        /*if (_tower != null)
        {
            if (!InBattle)
            {
                if (transform.position.x != _tower.transform.position.x && transform.position.z != _tower.transform.position.z)
                {
                    transform.LookAt(new Vector3(_tower.transform.position.x, transform.position.y, _tower.transform.position.z));
                    transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                }
            }
        }
        else
        {
            {
                _Towers = GameObject.FindGameObjectsWithTag("Archer_place");
                foreach (var item in _Towers)
                {
                    if (distanceTotower > Vector3.Distance(transform.position, item.transform.position))
                    {
                        distanceTotower = Vector3.Distance(transform.position, item.transform.position);
                        _tower = item;
                        towertarget = item.transform;
                    }
                }
            }
        }*/

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
            if (base.player.UserColor == "red")
            {
                if ((string)data[0] == "redUnit")
                {
                    goldConroller.KillTrophy(UnitName);
                }
            }
            else
            {
                if ((string)data[0] == "blueUnit")
                {
                    goldConroller.KillTrophy(UnitName);
                }
            }
        }
    }

    private void Attack()
    {
        // Переделать
        /*if (navMesh.isActiveAndEnabled)
        {
            if (!navMesh.isStopped)
            {
                navMesh.isStopped = true;
            }
        }*/
        navMesh.speed = 0;
        transform.LookAt(_gameObject.transform);
        _anim.SetBool("Hit", true);
        _gameObject.SendMessage("DealDamage", data);
        AttackTimer = coolDown;
        _arrow = Instantiate(arrowPrefab) as GameObject;
        _arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        _arrow.transform.rotation = transform.rotation;
    }
}
