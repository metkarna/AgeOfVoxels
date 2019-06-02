using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTower : Tower
{
    public float attackDistance;
    public float coolDown;
    public GameObject arrowPrefab;

    private GoldConroller goldConroller;
    private GameObject[] _Objects;
    private float distance = 1000f;
    private GameObject _gameObject;
    private Transform target;
    private object[] data;
    private float AttackTimer = 0;
    private GameObject _arrow;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        goldConroller = FindObjectOfType(typeof(GoldConroller)) as GoldConroller;
        _Objects = GameObject.FindGameObjectsWithTag(EnemyTag);
        foreach (var item in _Objects)
        {
            if (distance > Vector3.Distance(transform.position, item.transform.position))
            {
                distance = Vector3.Distance(transform.position, item.transform.position);
                _gameObject = item;
                target = item.transform;
            }
        }

        data = new object[2];
        data[0] = EnemyTag;
        data[1] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameObject != null)
        {
            if (Vector3.Distance(transform.position, _gameObject.transform.position) < attackDistance)
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
        else
        {
            distance = 1000f;
            _Objects = GameObject.FindGameObjectsWithTag(EnemyTag);
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

    private void Attack()
    {
        _gameObject.SendMessage("DealDamage", data);
        AttackTimer = coolDown;
        _arrow = Instantiate(arrowPrefab) as GameObject;
        _arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        _arrow.transform.LookAt(_gameObject.transform);
    }
}
