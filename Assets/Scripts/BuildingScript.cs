using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    private float dragSpeed = 0.01f;
    private float timeDragStarted;
    private Vector3 previousPosition = Vector3.zero;
    public float minForX = 0;
    public float minForZ = 0;
    public float maxForX = 0;
    public float maxForZ = 0;

    

    // Update is called once per frame
    void Update()
    {
        //тут может быть какое то условие, например на проверку состояния игры.
        //if (GameManager.CurrentGameState == GameState.Playing)
        //{

        
        

        //Нажимаем на экран
        if (Input.GetMouseButtonDown(0))
        {
            //Вычисляем расстояние  между начальными и текущими координатам
            Vector3 input = Input.mousePosition;
            float deltaX = (previousPosition.x - input.x) * dragSpeed;
            float deltaZ = (previousPosition.y - input.y) * dragSpeed;
            //Смотрим границы по X (добавить нормальное ограничение!!!)
            float newX = Mathf.Clamp(transform.position.x + deltaX, minForX, maxForX);
            //Смотрим границы по Y (добавить нормальное ограничение!!!)
            float newZ = Mathf.Clamp(transform.position.z + deltaZ, minForZ, maxForZ);
            //Задаем новые координаты для камеры
            transform.position = new Vector3(
                newX,
                transform.position.y,
                newZ
                );

            previousPosition = input;
            //для маленьких изменений увеличиваем 
            if (dragSpeed < 0.1f) dragSpeed += 0.002f;
        }
        // }
    }

}









