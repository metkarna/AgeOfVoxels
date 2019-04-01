using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    Rigidbody rb;
    public float speed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0, -4);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);    
    }

    void OnCollisionEnter(Collision col)
    {
       // if (col.gameObject.tag == "Enemy")
       // {
            transform.parent = col.transform;
            Destroy(this.gameObject);
        //}
        
    }



}
