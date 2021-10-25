using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Objects")]
    public Slider mouseSensitivitySlider;

    [Header("Values")]
    public float mouseSensitivity;

    private float _DEFAULT_MOUSE_SENSITIVITY = 100;

    // Update is called once per frame
    void Update()
    {
        mouseSensitivity = mouseSensitivitySlider.value;
    }
}
