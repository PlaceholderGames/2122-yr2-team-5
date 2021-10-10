using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
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
        SceneManager.LoadScene("map");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        mainMenuScreen.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void showMainMenu()
    {
        mainMenuScreen.SetActive(true);
        creditScreen.SetActive(false);
    }
}
