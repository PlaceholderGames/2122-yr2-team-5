using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : LoadingUI
{
    public void playAgain()
    {
        loadScene("map v2", GameObject.Find("UI").transform, GameObject.Find("UI").transform.Find("LoadingScreen"));
    }

    public void returnToMainMenu()
    {
        loadScene("MainMenu", GameObject.Find("UI").transform, GameObject.Find("UI").transform.Find("LoadingScreen"));
    }
}
