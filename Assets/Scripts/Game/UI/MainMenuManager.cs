using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.SceneManagement;

public class MainMenuManager : UIManager
{

    // Start is called before the first frame update
    void Start()
    {
        Transform canvas = GameObject.Find("Canvas").transform;

        hideScreen(canvas.Find("Settings").gameObject);
        showScreen(canvas.Find("MainMenu").gameObject);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("map v2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
