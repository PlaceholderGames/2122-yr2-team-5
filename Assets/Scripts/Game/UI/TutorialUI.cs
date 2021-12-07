using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : UIManager
{
    public GameObject[] pages;
    private int current_page = 0;

    void Start()
    {
        for(int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }

        pages[current_page].SetActive(true);
    }

    void next()
    {
        if (current_page < pages.Length)
        {
            pages[current_page].SetActive(false);
            current_page += 1;
            pages[current_page].SetActive(true);
        }
    }

    void previous()
    {
        if(current_page > 0)
        {
            pages[current_page].SetActive(false);
            current_page -= 1;
            pages[current_page].SetActive(true);
        }
    }
}
