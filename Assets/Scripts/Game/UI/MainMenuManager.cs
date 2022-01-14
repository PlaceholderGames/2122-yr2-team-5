using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : LoadingUI
{

    public Transform loadingScreen;

    Transform canvas;

    Transform levels;

    // Start is called before the first frame update
    void Start()
    {

        canvas = GameObject.Find("Canvas").transform;
        levels = canvas.Find("LevelSelection").Find("Levels");

        canPlayLevel();
        showScores();

        hideScreen(canvas.Find("LevelSelection"));
        hideScreen(canvas.Find("Settings"));
        hideScreen(canvas.Find("LoadingScreen"));
        showScreen(canvas.Find("MainMenu"));
    }

    private void showScores()
    {
        Color onStar = new Color(1, 0.9490197f, 0);
        Color offStar = new Color(0.6f, 0.6f, 0.6f);

        for(int l = 0; l < levels.childCount; l++)
        {
            Transform level = levels.GetChild(l);

            Transform stars = level.Find("Stars");
            string levelName = level.GetComponent<LevelData>().sceneName;
            int starScore = 0;

            if (PlayerPrefs.HasKey($"level.{levelName}.score")) starScore = PlayerPrefs.GetInt($"level.{levelName}.score");
            else starScore = 0;

            for (int s = 0; s < stars.childCount; s++)
            {
                GameObject star = stars.transform.GetChild(s).gameObject;
                star.GetComponent<Image>().color = s + 1 <= starScore ? onStar : offStar;
            }

        }
    }

    public void canPlayLevel()
    {
        for (int l = 1; l < levels.childCount; l++)
        {
            Transform prevLevel = levels.GetChild(l - 1);
            string prevLevelName = prevLevel.GetComponent<LevelData>().sceneName;

            Transform level = levels.GetChild(l);

            string prevLevelScore = $"level.{prevLevelName}.score";
            level.GetChild(0).GetComponent<UnityEngine.UI.Button>().interactable = PlayerPrefs.HasKey(prevLevelScore) && PlayerPrefs.GetInt(prevLevelScore) > 0;
        }
    }

    public void resetScores()
    {
        for (int l = 0; l < levels.childCount; l++)
        {
            Transform level = levels.GetChild(l);
            string levelName = level.GetComponent<LevelData>().sceneName;

            removeScore(levelName);
        }

        showScores();
        canPlayLevel();
    }

    private void removeScore(string sceneName)
    {
        string levelScore = $"level.{sceneName}.score";
        if (PlayerPrefs.HasKey(levelScore)) PlayerPrefs.DeleteKey(levelScore);
        PlayerPrefs.Save();
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
