using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : LoadingUI
{
    Transform UI;
    Transform LoadingScreen;

    private void Start()
    {
        UI = transform;
        LoadingScreen = transform.Find("LoadingScreen");
    }

    public void playAgain()
    {
        loadScene(SceneManager.GetActiveScene().name, UI, LoadingScreen);
    }

    public void next(string nextMapName)
    {
        loadScene(nextMapName, UI, LoadingScreen);
    }

    public void returnToMainMenu()
    {
        loadScene("MainMenu", UI, LoadingScreen);
    }
}
