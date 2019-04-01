using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour
{
    //урон
    public int damage = 1;
    //хп
    public int health = 5;
    //дальность видимости
    public float seeDistance = 50f;
    //дальность атаки
    public float attackDistance = 5f;
    //скорость
    public float speed = 3f;
    //тэг врага
    public string _tag;
    //скорость атаки
    public float coolDown;
    //затычка для нахождения ближнего врага
    private float distance = 1000f;
    // перемменные для объектов
    private Transform target;
    private GameObject[] _Objects;
    private GameObject _gameObject;

    private Animator _anim;
    //передаваемый объект с тэгом и уроном
    private object []data;
    //таймер для следующей атаки
    private float AttackTimer = 0;
    //ближний/дальний бой
    public bool archer;

    public GameObject arrowPrefab;

    private GameObject _arrow;

    // Start is called before the first frame update
    void Start()
    {
        //нахождение всех объектов с тэгом и запись в массив
        _Objects = GameObject.FindGameObjectsWithTag(_tag);
        //перебор для нахождения ближнего
        foreach (var item in _Objects)
        {
            if (distance > Vector3.Distance(transform.position, item.transform.position))
            {
                distance = Vector3.Distance(transform.position, item.transform.position);
                _gameObject = item;
                target = item.transform;
            }
        }

        
        //target = _gameObject.transform;
        
        _anim = GetComponent<Animator>();
        //добавление данных в объект
        data = new object[2];
        data[0] = _tag;
        data[1] = damage;
        if (archer)
        { 
            transform.LookAt(_gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                    //таймер для атаки
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
            //повторный поиск врага, когда цель убита
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
                if (archer && _gameObject != null)
                {
                    transform.LookAt(_gameObject.transform);
                }
            }
        }
        
    }

    private void DealDamage(object []_data)
    {
        if (health > 0)
        {
            health -= (int)_data[1];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        _anim.SetBool("Hit", true);
        _gameObject.SendMessage("DealDamage", data);
        AttackTimer = coolDown;
        if (archer)
        {
            _arrow = Instantiate(arrowPrefab) as GameObject;
            _arrow.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            _arrow.transform.rotation = transform.rotation;
        }
    }
}
