using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsColliding : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Is colliding");
    }
}
