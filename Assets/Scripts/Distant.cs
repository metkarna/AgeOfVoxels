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
    private GameObject enemy_castle;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        enemy_castle = GameObject.FindGameObjectWithTag("castle");
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!InBattle)
        {
            navMesh.SetDestination(enemy_castle.transform.position);
        }

        if (_gameObject != null)
        {
            if (Vector3.Distance(transform.position, _gameObject.transform.position) < seeDistance)
            {
                if (Vector3.Distance(transform.position, _gameObject.transform.position) > attackDistance)
                {
                    transform.LookAt(_gameObject.transform);
                    transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                    _anim.SetBool("Walk", true);
                }
                else
                {
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
        }
        else
        {
            InBattle = false;
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
                        InBattle = true;
                    }
                }
                if (_gameObject != null)
                {
                    transform.LookAt(_gameObject.transform);
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
        }
    }

    private void Attack()
    {
        // Переделать
        transform.LookAt(_gameObject.transform);
        _anim.SetBool("Hit", true);
        _gameObject.SendMessage("DealDamage", data);
        AttackTimer = coolDown;
        _arrow = Instantiate(arrowPrefab) as GameObject;
        _arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        _arrow.transform.rotation = transform.rotation;
    }
}
