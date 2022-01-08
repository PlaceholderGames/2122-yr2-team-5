using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : UIManager
{
    Slider loadingBar;

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void loadScene(string sceneName, Transform canvas, Transform loadingOverlay)
    {
        loadingBar = loadingOverlay.Find("Content").Find("Slider").GetComponent<Slider>();

        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            hideScreen(canvas.GetChild(i));
        }

        showScreen(loadingOverlay);
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    public void loadScene(int buildIndex, Transform canvas, Transform loadingOverlay)
    {
        loadingBar = loadingOverlay.Find("Content").Find("Slider").GetComponent<Slider>();

        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            hideScreen(canvas.GetChild(i));
        }

        showScreen(loadingOverlay);
        StartCoroutine(LoadSceneAsync(buildIndex));
    }

    IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;

            yield return null;
        }
    }

    IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;

            yield return null;
        }
    }
}
