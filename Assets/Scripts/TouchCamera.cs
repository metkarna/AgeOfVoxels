﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCamera : MonoBehaviour
{
Vector2?[] oldTouchPositions = {
        null,
        null
    };
    Vector2 oldTouchVector;
    float oldTouchDistance;
    Vector2 newTouchPosition;
 
    Vector2 screen;
 
    void Start()
    {
        screen = new Vector2(GetComponent<Camera>().pixelWidth, GetComponent<Camera>().pixelHeight);
    }
 
    void Update() {
        if (Input.touchCount == 0) {
            oldTouchPositions[0] = null;
            oldTouchPositions[1] = null;
        }
        
        else if (Input.touchCount == 1) {
            if (oldTouchPositions[0] == null || oldTouchPositions[1] != null) {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = null;
            }
            else {
                newTouchPosition = Input.GetTouch(0).position;
                
                transform.root.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * GetComponent<Camera>().orthographicSize / GetComponent<Camera>().pixelHeight * 2f));
                
                oldTouchPositions[0] = newTouchPosition;
            }
        }
        else {
            if (oldTouchPositions[1] == null) {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = Input.GetTouch(1).position;
                oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
                oldTouchDistance = oldTouchVector.magnitude;
            }
            else {
                
                
                Vector2[] newTouchPositions = {
                    Input.GetTouch(0).position,
                    Input.GetTouch(1).position
                };
                Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
                float newTouchDistance = newTouchVector.magnitude;
 
                transform.root.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * GetComponent<Camera>().orthographicSize / screen.y));
                transform.root.rotation *= Quaternion.Euler(new Vector3(0, Mathf.Asin(Mathf.Clamp((oldTouchVector.y * newTouchVector.x - oldTouchVector.x * newTouchVector.y) / oldTouchDistance / newTouchDistance, -1f, 1f)) / 0.0174532924f, 0));
                GetComponent<Camera>().orthographicSize *= oldTouchDistance / newTouchDistance;
                transform.root.position -= transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * GetComponent<Camera>().orthographicSize / screen.y);

                oldTouchPositions[0] = newTouchPositions[0];
                oldTouchPositions[1] = newTouchPositions[1];
                oldTouchVector = newTouchVector;
                oldTouchDistance = newTouchDistance;
            }
        }
    }
}
