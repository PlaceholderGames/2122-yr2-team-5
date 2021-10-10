using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameManager : MonoBehaviour
{
    public HighlightPlus.HighlightProfile highlightProfile;

    public TMPro.TextMeshProUGUI TimerUI;

    public float timerInSeconds = (5 * 60);

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] findable = GameObject.FindGameObjectsWithTag("FindMe");
        for (int i = 0; i < findable.Length; i++)
        {
            findable[i].AddComponent<HighlightPlus.HighlightEffect>();
            findable[i].AddComponent<HighlightPlus.HighlightTrigger>();

            findable[i].GetComponent<HighlightPlus.HighlightEffect>().profile = highlightProfile;
            findable[i].GetComponent<HighlightPlus.HighlightEffect>().profileSync = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Timer system
        if (timerInSeconds > 0)
        {
            timerInSeconds -= Time.deltaTime;
        } else
        {
            // Load Game Over Screen
            timerInSeconds = 0;
        }

        displayTime(timerInSeconds);
    }

    void displayTime(float time)
    {
        if(time < 0)
        {
            time = 0;
        }

        if (time <= 60)
        {
            TimerUI.color = new Color(1, 0, 0);
        } else
        {
            TimerUI.color = new Color(1, 1, 1);
        }

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        TimerUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
