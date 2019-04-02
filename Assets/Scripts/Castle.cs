using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public string Enemy_Tag;
    public int Castle_Hitpoint;
    private GameObject _enemy;

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == Enemy_Tag)
        {
            Destroy(collision.gameObject);
            Castle_Hitpoint--;
            if (Castle_Hitpoint == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
