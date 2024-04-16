using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSettings
{
    private static float _mouseSensitivity = 100.0f; // Default sensitivity
    public static float MouseSensitivity
    {
        get
        {
            return _mouseSensitivity;
        }
        set
        {
            _mouseSensitivity = value;
            PlayerPrefs.SetFloat("MouseSensitivity", value);
            PlayerPrefs.Save();
        }
    }

    public static void LoadSettings()
    {
        _mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 100.0f);
    }
}