using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.SceneManagement;

public class MainMenuManager : UIManager
{
    public GameObject creditScreen;
    public GameObject mainMenuScreen;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuScreen.SetActive(true);
        creditScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        DontDestroyOnLoad(GameObject.Find("GameSettings"));
        SceneManager.LoadScene("map");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
