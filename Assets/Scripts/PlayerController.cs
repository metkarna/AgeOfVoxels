using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    public Vector3 targetPos;
    public bool isAttack = false;
    private NavMeshAgent _agent;
    private Animator _anim;
   
    void Start()
    {
        targetPos = transform.position;
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
       // OnAnimatorMove();
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if( Physics.Raycast( ray, out hit, 100 ) )
            {
                GameObject Ground = hit.transform.gameObject;
                if (Ground.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                    targetPos = hit.point;
                }
            }
        }

        if (!isAttack)
        {
            GetComponent<Animator>().SetBool("Walk", _agent.velocity != Vector3.zero);
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (isAttack)
            {
                _anim.SetBool("Hit", false);
                isAttack = false;
            }
            else
            {
                isAttack = true;
                targetPos = transform.position;
                _agent.velocity = Vector3.zero;
                _anim.SetBool("Hit", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //_anim.SetBool("Death", true);
            this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            _anim.Play("Death");
             Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            //OnAnimatorMove();
            //_anim.Play(animDie.name);
            //Destroy(this.gameObject, animDie.length);
            //_anim.SetBool("Death", true);
            //_anim.enabled = false;
            
            //_anim.SetTrigger("Die");
            //Destroy(this.gameObject, 5.5f);
            //Destroy(this);
        }

        _agent.SetDestination(targetPos);
    }
    
   /* void OnAnimatorMove(){
        transform.rotation = _anim.rootRotation;
        transform.position = _agent.nextPosition;
    }*/
}
