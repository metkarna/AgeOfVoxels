using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    
    public GameObject person;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if( Physics.Raycast( ray, out hit, 100 ) )
            {
                GameObject spawner = hit.transform.gameObject;
                if (spawner.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity) &&
                    spawner.CompareTag("castle"))
                {
                    //spawner.SetActive(false);
                    Instantiate (person, Vector3.zero, spawner.transform.rotation);
                }
            }
        }
    }
}
