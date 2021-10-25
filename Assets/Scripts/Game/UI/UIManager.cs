using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
