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
        	// Уничтожаем персонажа
            Destroy(collision.gameObject);
            Castle_Hitpoint--;
            if (Castle_Hitpoint == 0)
            {
            	// Уничтожаем базу
            	var ui = GameObject.FindObjectOfType(typeof(UIController)) as UIController;
            	string msg = gameObject.tag == "castle" ? "Победа :)": "Поражение :(";
            	ui.CastleDestroy(msg);
            	Destroy(gameObject);
                // Заканчиваем игру
            }
        }
    }
}
