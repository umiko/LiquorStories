﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NozzleTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Nuzzle: " + other.name);
    }
}