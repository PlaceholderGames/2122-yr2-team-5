using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : UIManager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSliderValue(GameObject go)
    {
        Transform component = go.transform.Find("Slider");
        Transform componentValue = component.transform.Find("Value");

        componentValue.GetComponent<TMPro.TextMeshProUGUI>().text = component.GetComponent<Slider>().value.ToString();
    }
}
