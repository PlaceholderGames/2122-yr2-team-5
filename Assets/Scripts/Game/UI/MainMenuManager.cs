using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : LoadingUI
{

    public Transform loadingScreen;

    Transform canvas;
    Slider loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;

        hideScreen(canvas.Find("Settings").gameObject);
        hideScreen(canvas.Find("LoadingScreen").gameObject);
        showScreen(canvas.Find("MainMenu").gameObject);
    }

    public void PlayGame()
    {
        loadScene("map v2", canvas, loadingScreen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
