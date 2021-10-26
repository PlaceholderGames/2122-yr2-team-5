using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Values")]
    public float mouseSensitivity = 100.0f;
    public List<bool> invertedMouse = new List<bool> { false, false };

    private void Start()
    {
        if (PlayerPrefs.HasKey("mouse.sensitivity")) mouseSensitivity = PlayerPrefs.GetFloat("mouse.sensitivity");
        if (PlayerPrefs.HasKey("mouse.invert.x")) invertedMouse[0] = intToBool(PlayerPrefs.GetInt("mouse.invert.x"));
        if (PlayerPrefs.HasKey("mouse.invert.y")) invertedMouse[1] = intToBool(PlayerPrefs.GetInt("mouse.invert.y"));

        if(!(PlayerPrefs.HasKey("mouse.sensitivity") && PlayerPrefs.HasKey("mouse.invert.x") && PlayerPrefs.HasKey("mouse.invert.y")))
        {
            save();
        }
    }

    // Update is called once per frame
    int boolToInt(bool b)
    {
        return b ? 1 : 0;
    }

    bool intToBool(int i)
    {
        return i == 1;
    }

    public void save()
    {
        PlayerPrefs.SetFloat("mouse.sensitivity", mouseSensitivity);
        PlayerPrefs.SetInt("mouse.invert.x", boolToInt(invertedMouse[0]));
        PlayerPrefs.SetInt("mouse.invert.y", boolToInt(invertedMouse[1]));
        PlayerPrefs.Save();
    }
}
