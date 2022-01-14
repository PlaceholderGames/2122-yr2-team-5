using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : LoadingUI
{
    Transform UI;
    Transform LoadingScreen;
    GameManager gm;

    private void Start()
    {
        UI = transform;
        LoadingScreen = transform.Find("LoadingScreen");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        showContinueButton();
    }

    public void playAgain()
    {
        loadScene(SceneManager.GetActiveScene().name, UI, LoadingScreen);
    }

    public void next()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentSceneIndex + 1;

        saveScore();
        loadScene(nextLevel, UI, LoadingScreen);
    }

    public void returnToMainMenu()
    {
        saveScore();
        loadScene("MainMenu", UI, LoadingScreen);
    }

    private void showContinueButton()
    {
        Transform button = UI.Find("GameOver").Find("Buttons").Find("Next");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentSceneIndex + 1;

        bool hasAnotherLevel = nextLevel <= SceneManager.sceneCountInBuildSettings - 1;
        button.GetComponent<Button>().interactable = gm.stars > 0;

        button.gameObject.SetActive(hasAnotherLevel);
    }
    public void saveScore()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string levelName = $"level.{sceneName}";
        bool hasScore = PlayerPrefs.HasKey(levelName + ".score");
        if(hasScore)
        {
            if(gm.starScore >= PlayerPrefs.GetInt(levelName + ".score")) {
                PlayerPrefs.SetInt(levelName + ".score", gm.starScore);
                PlayerPrefs.Save();
            }
        } else
        {
            PlayerPrefs.SetInt(levelName + ".score", gm.starScore);
            PlayerPrefs.Save();
        }
    }
}
