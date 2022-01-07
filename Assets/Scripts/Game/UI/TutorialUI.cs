using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : UIManager
{
    public GameObject[] pages;
    public GameObject[] buttons;
    

    [SerializeField]
    private int current_page = 0;
    private GameManager gm;
    private PlayerController player;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = gm.getPlayer() != null ? gm.getPlayer() : GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        pages[current_page].SetActive(true);

        string interactText = pages[3].transform.Find("Content").Find("Description").GetComponent<TMPro.TextMeshProUGUI>().text;
        if (interactText.Contains("[Key]"))
        {
            string replacedText = interactText.Replace("[Key]", player.interactKey.ToString());
            Transform description = pages[3].transform.Find("Content").Find("Description");
            TMPro.TextMeshProUGUI textComponent = description.GetComponent<TMPro.TextMeshProUGUI>();
            if(textComponent)
            {
                textComponent.text = replacedText;
            }
        }
    }

    private void Update()
    {
        if (current_page == 0) buttons[0].SetActive(false);
        else buttons[0].SetActive(true);

        if (current_page == pages.Length - 1)
        {
            buttons[1].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Finish";
        }
    }

    public void next()
    {
        /*Debug.Log(current_page);
        Debug.Log(pages.Length);*/

        if (current_page == pages.Length - 1)
        {
            finish();
            return;
        }

        if (current_page < pages.Length)
        {
            pages[current_page].SetActive(false);
            current_page += 1;
            pages[current_page].SetActive(true);
        }
    }

    public void previous()
    {
        if(current_page > 0)
        {
            pages[current_page].SetActive(false);
            current_page -= 1;
            pages[current_page].SetActive(true);
        }
    }

    void finish()
    {
        current_page = 0;
        gm.finishedTutorial = true;
    }
}
