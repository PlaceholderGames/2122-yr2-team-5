using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class TrafficLightNode : MonoBehaviour
{
    TrafficLightController controller;
    Light nodeLight;

    [Header("Colours")]
    public Color stop = Color.red;
    public Color warning = new Color(1.0f, 0.65f, 0, 1);
    public Color go = Color.green;

    public bool automateTag = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<TrafficLightController>();
        nodeLight = GetComponent<Light>();
        if(automateTag) transform.tag = "trafficLightNode";
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.currentState == TrafficLightState.GO) nodeLight.color = go;
        else if (controller.currentState == TrafficLightState.WARNING) nodeLight.color = warning;
        else if (controller.currentState == TrafficLightState.STOP) nodeLight.color = stop;
        else nodeLight.color = Color.black;

    }
}
