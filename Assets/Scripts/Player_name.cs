using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_name : MonoBehaviour
{

    public GameObject pl_name;

    private Text pl_name_text;

    // Start is called before the first frame update
    void Start()
    {
        System.Random random = new System.Random();
        string userid = "Guest" + random.Next(0, 10000);
        pl_name_text = pl_name.GetComponent<Text>();
        pl_name_text.text = userid;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
