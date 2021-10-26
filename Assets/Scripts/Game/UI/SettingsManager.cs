using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : UIManager
{
    public Settings settingsManager;
    public GameObject mouseSensitivity;
    public List<Toggle> mouseInvert;

    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity.transform.Find("Slider").GetComponent<Slider>().value = PlayerPrefs.GetFloat("mouse.sensitivity");
        for(int i = 0; i < mouseInvert.Count; i++)
        {
            string axis = "";
            switch(i)
            {
                case 0:
                    axis += "x";
                    break;
                case 1:
                    axis += "y";
                    break;
            }
            mouseInvert[i].isOn = PlayerPrefs.GetInt("mouse.invert." + axis) == 1 ? true : false;
        }

        showSliderValue(mouseSensitivity);
        onBackClicked();
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void onBackClicked()
    {
        settingsManager.mouseSensitivity = mouseSensitivity.transform.Find("Slider").GetComponent<Slider>().value;
        for (int i = 0; i < mouseInvert.Count; i++)
        {
            settingsManager.invertedMouse[i] = mouseInvert[i].isOn;
        }
        settingsManager.save();
    }

    public void showSliderValue(GameObject go)
    {
        Transform component = go.transform.Find("Slider");
        Transform componentValue = component.transform.Find("Value");

        componentValue.GetComponent<TMPro.TextMeshProUGUI>().text = component.GetComponent<Slider>().value.ToString();
    }
}
