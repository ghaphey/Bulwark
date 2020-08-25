using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Health wallHealth;
    [SerializeField] private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        startScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wallHealth.IsAlive() || !playerHealth.IsAlive())
            DeathScreen();
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (Time.timeScale != 0f)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        startScreen.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    private void DeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
