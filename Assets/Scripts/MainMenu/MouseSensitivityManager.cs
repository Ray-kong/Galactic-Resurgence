using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivityManager : MonoBehaviour
{
    public Slider sensitivitySlider;
    public float sensitivityScale = 200.0f;

    private void Start()
    {
        GameSettings.LoadSettings();
        sensitivitySlider.value = GameSettings.MouseSensitivity / sensitivityScale;
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnSensitivityChanged(float sliderValue)
    {
        GameSettings.MouseSensitivity = sliderValue * sensitivityScale;
    }
}