using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : LoadingUI
{
    Transform UI;
    Transform LoadingScreen;

    private void Start()
    {
        UI = GameObject.Find("UI").transform;
        LoadingScreen = GameObject.Find("UI").transform.Find("LoadingScreen");
    }

    public void playAgain()
    {
        loadScene("map v2", UI, LoadingScreen);
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
