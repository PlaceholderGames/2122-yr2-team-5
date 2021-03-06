using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using HighlightPlus;
using TMPro;

using UnityEngine.SceneManagement;

public class GameManager : UIManager
{
    public HighlightPlus.HighlightProfile highlightProfile;
    public bool isTutorial;

    public float timeInSeconds = 0;

    public GameObject UI;
    GameObject GameUI;
    GameObject TutorialUI;
    GameObject GameOverUI;
    GameObject PauseUI;
    GameObject SettingsUI;
    GameObject LoadingUI;

    GameObject TimerUI;

    GameObject CollectUI;
    GameObject CollectablesUI;
    GameObject RoomUI;

    [Header("Interaction Distance")]
    public float raycastDistance = 4f;

    bool gameOver;
    bool paused;
    bool showCollectables;

    GameObject player;
    PlayerController playerController;
    ObjectController objectController;

    [HideInInspector]
    public bool finishedTutorial = false;
    public bool completedTutorial;

    [HideInInspector]
    public GameObject star;

    [SerializeField]
    private float difficulty = 1.25f;

    [HideInInspector]
    public int starScore = 0;

    [HideInInspector]
    public int stars =  0;

    [HideInInspector]
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        GameUI = UI.transform.Find("Game").gameObject;
        TutorialUI = UI.transform.Find("Tutorial").gameObject;
        GameOverUI = UI.transform.Find("GameOver").gameObject;
        PauseUI = UI.transform.Find("Paused").gameObject;
        SettingsUI = UI.transform.Find("Settings").gameObject;
        LoadingUI = UI.transform.Find("LoadingScreen").gameObject;

        TimerUI = GameUI.transform.Find("Timer").gameObject;
        CollectUI = GameUI.transform.Find("Collect").gameObject;
        CollectablesUI = GameUI.transform.Find("Objects").gameObject;
        RoomUI = GameUI.transform.Find("Room").gameObject;

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        CollectUI.transform.Find("Text").Find("Key").GetComponent<TMPro.TextMeshProUGUI>().text = "Press " + playerController.interactKey + " to collect";

        gameOver = false;
        paused = false;
        showCollectables = true;

        objectController = GetComponent<ObjectController>();

        GameUI.gameObject.SetActive(true);
        GameOverUI.gameObject.SetActive(false);
        PauseUI.gameObject.SetActive(false);
        SettingsUI.gameObject.SetActive(false);
        LoadingUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        stars = calculateStars();


        if(paused)
        {
            Time.timeScale = 0;
        } else
        {
            if (isTutorial)
            {
                if (finishedTutorial) Time.timeScale = 1;
                else Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if(isTutorial)
        {
            if (finishedTutorial) hideScreen(TutorialUI.transform);
            else showScreen(TutorialUI.transform);
        } else
        {
            hideScreen(TutorialUI.transform);
        }

        completedTutorial = isTutorial ? finishedTutorial : true;


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

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                showCollectables = !showCollectables;
            }

            if (player.transform.position != playerController.lastPosition && GetComponent<TrafficLightController>().currentState == TrafficLightState.STOP)
            {
                timeInSeconds += (GetComponent<TrafficLightController>().playerMoveTime) * Time.deltaTime * Time.timeScale;
            }

            updateTime();
        }

        CollectablesUI.gameObject.SetActive(showCollectables);

        if (objectController.collectedAll())
        {
            gameOver = true;
            displayGameOver(objectController.collectedAll(), timeInSeconds);
        }

        displayTime(timeInSeconds);
        showPauseScreen(paused);
        showRoom(playerController.currentRoom);
    }

    public PlayerController getPlayer()
    {
        return playerController;
    }

    public void setPaused(bool value)
    {
        this.paused = value;
    }

    public void showCollectUI(bool show)
    {
        CollectUI.SetActive(show);
    }

    public void showCollectUIAtTransform(bool show, Transform point)
    {
        CollectUI.transform.Find("Text").Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = point.transform.name;
        Vector3 position = Camera.main.WorldToScreenPoint(point.position, Camera.MonoOrStereoscopicEye.Mono);
        CollectUI.transform.position = position;
        showCollectUI(show);
    }

    public void showPauseScreen(bool show)
    {
        showCollectUI(!show);
        GameUI.gameObject.SetActive(!show && !gameOver);
        PauseUI.gameObject.SetActive(show);
    }

    void showRoom(string roomName)
    {
        RoomUI.gameObject.SetActive(!(roomName == "None"));
        RoomUI.transform.Find("Value").GetComponent<TMPro.TextMeshProUGUI>().text = roomName;
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

    int calculateStars()
    {
        // Visual graph of star calculation
        // https://www.desmos.com/calculator/t8lq8kbzms

        float f = objectController.objectsToFind;
        float r = this.difficulty;

        float c = (f / (timeInSeconds / 60));
        float y = (c / (f / (f * r)));

        return Mathf.FloorToInt(y);
    }

    void displayGameOver(bool collectedAll, float timeLeft)
    {
        GameUI.SetActive(false);

        starScore = calculateStars();
        Color onStar = new Color(1, 0.9490197f, 0);
        Color offStar = new Color(0.6f, 0.6f, 0.6f);

        GameObject stars = GameOverUI.transform.Find("Stars").gameObject;
        GameObject timeText = GameOverUI.transform.Find("Time").gameObject;

        for(int s = 0; s < stars.transform.childCount; s++)
        {
            GameObject star = stars.transform.GetChild(s).gameObject;
            star.GetComponent<Image>().color = s + 1 <= starScore ? onStar : offStar;
        }

        bool isSuccess = collectedAll;

        float minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float seconds = Mathf.FloorToInt(timeInSeconds % 60);
        timeText.GetComponent<TextMeshProUGUI>().text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);

        GameOverUI.gameObject.SetActive(true);
    }

    void updateTime()
    {
        if (timeInSeconds < 0) timeInSeconds = 0;
        if (!objectController.collectedAll())
        {
            timeInSeconds += (Time.deltaTime * Time.timeScale);
        }
    }

    void displayTime(float time)
    {
        TMPro.TextMeshProUGUI timerText = TimerUI.transform.Find("Value").GetComponent<TMPro.TextMeshProUGUI>();

        if (time < 0) time = 0;

        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

}
