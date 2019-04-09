using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public int damage;
    public int health;
    public float seeDistance;
    public float attackDistance;
    public float speed;
    public string _tag;
    public float coolDown;
    public bool InBattle;
    public string UnitName;

    protected float distance = 1000f;
    protected Transform target;
    protected GameObject[] _Objects;
    protected GameObject _gameObject;
    protected Animator _anim;
    protected object[] data;
    protected float AttackTimer = 0;
    


    // Start is called before the first frame update
    protected void Start()
    {
        _Objects = GameObject.FindGameObjectsWithTag(_tag);
        foreach (var item in _Objects)
        {
            if (distance > Vector3.Distance(transform.position, item.transform.position))
            {
                distance = Vector3.Distance(transform.position, item.transform.position);
                _gameObject = item;
                target = item.transform;
                //InBattle = true;
                
            }
        }

        _anim = GetComponent<Animator>();
        data = new object[2];
        data[0] = _tag;
        data[1] = damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
