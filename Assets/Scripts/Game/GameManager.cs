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
    public Transform PauseUI;
    public Transform SettingsUI;
    public Transform LoadingUI;
    Transform TimerUI;
    Transform CollectUI;
    Transform CollectablesUI;
    Transform RoomUI;

    [Header("Interaction Distance")]
    public float raycastDistance = 4f;

    bool gameOver;
    bool paused;
    bool showCollectables;

    Controller playerController;
    ObjectController objectController;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        paused = false;
        showCollectables = true;

        objectController = GetComponent<ObjectController>();

        for (int i = 0; i < objectController.objects.Count; i++)
        {
            HighlightEffect highlight = objectController.objects[i].AddComponent<HighlightEffect>();
            HighlightTrigger trigger = objectController.objects[i].AddComponent<HighlightTrigger>();
            if (!objectController.objects[i].GetComponent<ObjectProperty>()) objectController.objects[i].AddComponent<ObjectProperty>();

            highlight.ProfileLoad(highlightProfile);
        }

        TimerUI = GameUI.Find("Timer");
        CollectUI = GameUI.Find("Collect");
        CollectablesUI = GameUI.Find("Objects");
        RoomUI = GameUI.Find("Room");

        playerController = GameObject.Find("Player").GetComponent<Controller>();
        CollectUI.Find("Label").GetComponent<TMPro.TextMeshProUGUI>().text = "Press " + playerController.interactKey + " to collect";

        GameUI.gameObject.SetActive(true);
        GameOverUI.gameObject.SetActive(false);
        PauseUI.gameObject.SetActive(false);
        SettingsUI.gameObject.SetActive(false);
        LoadingUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused) Time.timeScale = 0;
        else Time.timeScale = 1;

        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
                if (!paused && (SettingsUI.gameObject.activeInHierarchy || SettingsUI.gameObject.activeSelf))
                {
                    SettingsUI.GetComponent<SettingsManager>().resetValues();
                    SettingsUI.gameObject.SetActive(paused);
                }
            }

            if(Input.GetKeyDown(KeyCode.Tab))
            {
                showCollectables = !showCollectables;
            }

            if (playerController.transform.position != playerController.lastPosition && GetComponent<TrafficLightController>().currentState == TrafficLightState.STOP)
            {
                timerInSeconds -= GetComponent<TrafficLightController>().playerMoveTime * Time.deltaTime;
            }

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
        }

        CollectablesUI.gameObject.SetActive(showCollectables);

        if(timerInSeconds <= 0 || objectController.collectedAll())
        {
            gameOver = true;
            displayGameOver(objectController.collectedAll(), timerInSeconds);
        }

        displayTime(timerInSeconds);
        showPauseScreen(paused);
        showRoom(playerController.currentRoom);
    }




    public void showCollectUI(bool show)
    {
        CollectUI.gameObject.SetActive(show);
    }

    public void showPauseScreen(bool show)
    {
        showCollectUI(!show);
        GameUI.gameObject.SetActive(!show);
        PauseUI.gameObject.SetActive(show);
    }

    void showRoom(string roomName)
    {
        RoomUI.gameObject.SetActive(!(roomName == "None"));
        RoomUI.Find("Value").GetComponent<TMPro.TextMeshProUGUI>().text = roomName;
    }

    public bool isGameOver()
    {
        return this.gameOver;
    }

    public void resume()
    {
        paused = false;
    }

    public void loadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.name);
    }

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public bool isPaused()
    {
        return paused;
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

        GameUI.gameObject.SetActive(false);
        GameOverUI.gameObject.SetActive(true);
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
