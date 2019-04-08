using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Gold { get; set; }

    public int Name { get; set; }
    private const int defaultGoldValue = 300;
    private void Start()
    {
        Gold = defaultGoldValue;
    }
}
