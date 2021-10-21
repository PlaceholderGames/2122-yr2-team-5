using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using HighlightPlus;
using TMPro;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HighlightPlus.HighlightProfile highlightProfile;

    [Header("Timer")]
    public float timerInSeconds = (5 * 60);


    [Header("UI")]
    public Transform GameUI;
    public Transform GameOverUI;
    Transform TimerUI;
    Transform CollectUI;

    [Header("Interaction Distance")]
    public float raycastDistance = 4f;

    bool gameOver;

    ObjectController objectController;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;

        GameObject[] findable = GameObject.FindGameObjectsWithTag("FindMe");
        for (int i = 0; i < findable.Length; i++)
        {
            HighlightEffect highlight = findable[i].AddComponent<HighlightEffect>();
            HighlightTrigger trigger = findable[i].AddComponent<HighlightTrigger>();
            if(!findable[i].GetComponent<ObjectProperty>()) {
                findable[i].AddComponent<ObjectProperty>();
            }

            highlight.ProfileLoad(highlightProfile);
        }

        objectController = GetComponent<ObjectController>();

        TimerUI = GameUI.Find("Timer");
        CollectUI = GameUI.Find("Collect");


        CollectUI.Find("Label").GetComponent<TMPro.TextMeshProUGUI>().text = "Press " + GameObject.Find("Player").GetComponent<Controller>().interactKey + " to collect";

        GameOverUI.gameObject.SetActive(gameOver);
        GameUI.gameObject.SetActive(!gameOver);
    }

    // Update is called once per frame
    void Update()
    {


        // Timer system
        if (timerInSeconds > 0)
        {
            if (!objectController.collectedAll())
            {
                timerInSeconds -= Time.deltaTime;
            }
        }
        else
        {
            timerInSeconds = 0;
        }

        if(timerInSeconds <= 0 || objectController.collectedAll())
        {
            gameOver = true;
            displayGameOver(objectController.collectedAll(), timerInSeconds);
        }

        displayTime(timerInSeconds);
    }

    public void showCollectUI(bool show)
    {
        CollectUI.gameObject.SetActive(show);
    }

    public bool isGameOver()
    {
        return this.gameOver;
    }

    public void playAgain()
    {
        SceneManager.LoadScene("map");
    }

    public void backToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void displayGameOver(bool collectedAll, float timeLeft)
    {
        bool isSuccess = collectedAll && timeLeft > 0;

        GameObject stateText = GameOverUI.Find("State").gameObject;
        GameObject timeText = GameOverUI.Find("Time").gameObject;
        GameObject itemText = GameOverUI.Find("Items").gameObject;

        stateText.GetComponent<TextMeshProUGUI>().text = isSuccess ? "You collected all the items!" : "You didn't collect all the items";

        float minutes = Mathf.FloorToInt(timerInSeconds / 60);
        float seconds = Mathf.FloorToInt(timerInSeconds % 60);
        timeText.GetComponent<TextMeshProUGUI>().text = string.Format("Time remaining: {0:00}:{1:00}", minutes, seconds);

        itemText.GetComponent<TextMeshProUGUI>().text = "Objects collected: " + objectController.getCollectedObjects();

        // Add information

        GameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
    }

    void displayTime(float time)
    {
        TMPro.TextMeshProUGUI timerText = TimerUI.Find("Value").GetComponent<TMPro.TextMeshProUGUI>();

        if (time < 0) time = 0;

        if (time <= 60) timerText.color = new Color(1, 0, 0);
        else timerText.color = new Color(1, 1, 1);

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
