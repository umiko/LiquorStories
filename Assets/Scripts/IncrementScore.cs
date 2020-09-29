using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IncrementScore : MonoBehaviour
{
    public string scoreString;
    private int score;
    private TextMeshPro tmp;

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        scoreString = "Drinks Served: {0}";
        score = -1;
        Increment();
    }

    // Start is called before the first frame update
    public void Increment()
    {
        score++;
        tmp.text = String.Format(scoreString, score);
    }
}
