using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject teamPanel;
    public GameObject sensitivityPanel;
    public GameObject buttonsAndText;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

    public void ShowTeam()
    {
        teamPanel.SetActive(true);  // Show the team panel
        buttonsAndText.SetActive(false);
    }

    public void HideTeam()
    {
        teamPanel.SetActive(false);  // Hide the team panel
        buttonsAndText.SetActive(true);
    }

    public void ShowSensitivity()
    {
        sensitivityPanel.SetActive(true);
        buttonsAndText.SetActive(false);
    }

    public void HideSensitivity()
    {
        sensitivityPanel.SetActive(false);
        buttonsAndText.SetActive(true);
    }
}