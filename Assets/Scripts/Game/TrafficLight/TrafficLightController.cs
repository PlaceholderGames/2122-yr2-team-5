using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public TrafficLightState currentState;

    [Header("Time (seconds)")]
    public float timerInSeconds;

    [Tooltip("At what point in the timer should change in state")]
    public float warningTime = 6f;
    public float stopTime = 5f;
    public float goTime = 0f;

    List<TrafficLightNode> lights = new List<TrafficLightNode>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] trafficLights = GameObject.FindGameObjectsWithTag("trafficLightNode");
        for (int i = 0; i < trafficLights.Length; i++)
        {
            if(trafficLights[i].GetComponent<TrafficLightNode>()) trafficLights[i].GetComponent<TrafficLightNode>();
            else trafficLights[i].AddComponent<TrafficLightNode>();
            lights.Add(trafficLights[i].GetComponent<TrafficLightNode>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Timer system
        if (timerInSeconds > 0)
        {
            timerInSeconds -= Time.deltaTime;
        }
        else
        {
            timerInSeconds = 20;
        }

        if(timerInSeconds > warningTime)
        {
            currentState = TrafficLightState.GO;
        } else if(timerInSeconds > stopTime && timerInSeconds <= warningTime)
        {
            currentState = TrafficLightState.WARNING;
        } else if(timerInSeconds > goTime && timerInSeconds <= stopTime)
        {
            currentState = TrafficLightState.STOP;
        }
    }
}

public enum TrafficLightState
{
    STOP,
    WARNING,
    GO
}
