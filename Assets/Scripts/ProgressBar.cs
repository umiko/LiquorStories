using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    private float fillSpeed = 0.5f;
    private float targetProgress = 0;

    public bool IsComplete
    {
        get;
        private set;
    }

    public TextMeshProUGUI progressText;

    private void Start()
    {
        slider = GetComponent<Slider>();
        //progressText = FindObjectOfType<TextMeshProUGUI>();
        progressText = GetComponentInChildren<TextMeshProUGUI>();
        progressText.text = "Shake to mix";
    }

    // Update is called once per frame
    private void Update()
    {
        //IncrementProgress(Time.deltaTime * 0.01f);
        //Debug.Log("slider " + slider.gameObject.name);
        if (!IsComplete)
        {
            if (slider.value < targetProgress)
            {
                slider.value += fillSpeed * Time.deltaTime;
                //text.text = slider.value >= 1 ? "Drink XY" : Math.Round(slider.value * 100f, 2) + "%";
                progressText.text = Math.Round(slider.value * 100f, 2) + "%";
            }
            if (slider.value >= 1)
            {
                IsComplete = true;
            }
        }
    }

    public void IncrementProgress(float progress)
    {
        //targetProgress = Mathf.Clamp(slider.value + progress, 0f, 1f);
        //if(slider.value + progress < 1)
        targetProgress = Mathf.Clamp01(slider.value + progress / .9f);
        //Debug.Log("Target " + targetProgress);
    }

    public void Reset()
    {
        progressText.text = "Shake to mix";
        slider.value = 0;
        targetProgress = 0;
        IsComplete = false;
    }
}