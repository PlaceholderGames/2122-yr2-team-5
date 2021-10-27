using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : UIManager
{
    Slider loadingBar;

    public void loadScene(string sceneName, Transform canvas, Transform loadingOverlay)
    {
        loadingBar = loadingOverlay.Find("Content").Find("Slider").GetComponent<Slider>();

        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            hideScreen(canvas.GetChild(i).gameObject);
        }

        showScreen(loadingOverlay.gameObject);
        StartCoroutine(LoadSceneAsync(sceneName));
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
}
