using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPrefab : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fillImage;

    void Start()
    {
        SetStartingPosition(10f);
    }

    public void SetStartingPosition(float maxDistance)
    {
        slider.maxValue = maxDistance;
        slider.value = 0f;
        //fillImage.color = gradient.Evaluate(0f);
    }

    public void SetCurrentPosition(float currentDistance)
    {
        slider.value = currentDistance;
        //fillImage.color = gradient.Evaluate(slider.normalizedValue);
    }
}