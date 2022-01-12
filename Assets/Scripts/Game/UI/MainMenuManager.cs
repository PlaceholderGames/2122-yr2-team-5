using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuManager : LoadingUI
{

    public Transform loadingScreen;

    Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;

        hideScreen(canvas.Find("LevelSelection"));
        hideScreen(canvas.Find("Settings"));
        hideScreen(canvas.Find("LoadingScreen"));
        showScreen(canvas.Find("MainMenu"));
        showScores();
    }

    private void showScores()
    {
        Color onStar = new Color(1, 0.9490197f, 0);
        Color offStar = new Color(0.6f, 0.6f, 0.6f);

        Transform levels = canvas.Find("LevelSelection").Find("Levels");
        for(int l = 0; l < levels.childCount; l++)
        {
            Transform level = levels.GetChild(l);

            Transform stars = level.Find("Stars");
            string levelName = level.GetComponent<LevelData>().sceneName;
            int starScore = 0;

            if (PlayerPrefs.HasKey($"level.{levelName}.score")) starScore = PlayerPrefs.GetInt($"level.{levelName}.score");

            for (int s = 0; s < stars.childCount; s++)
            {
                GameObject star = stars.transform.GetChild(s).gameObject;
                star.GetComponent<Image>().color = s + 1 <= starScore ? onStar : offStar;
            }

        }
    }

    public void PlayLevel()
    {
        Transform playButton = EventSystem.current.currentSelectedGameObject.transform;
        string levelName = playButton.parent.GetComponent<LevelData>().sceneName;
        loadScene(levelName, canvas, loadingScreen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    
}
