using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void showScreen(Transform transform)
    {
        transform.gameObject.SetActive(true);
    }
    public void hideScreen(Transform transform)
    {

        transform.gameObject.SetActive(false);
    }
}
