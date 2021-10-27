using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void showScreen(GameObject screen)
    {
        screen.SetActive(true);
    }
    public void hideScreen(GameObject screen)
    {
        screen.SetActive(false);
    }
}
