using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public string EnemyTag;

    protected Vector3 TowerPosition;
    protected Player player;
    protected string PlayerColor;

    // Start is called before the first frame update
    protected void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player)) as Player;
        PlayerColor = player.UserColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
