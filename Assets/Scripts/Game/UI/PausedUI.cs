using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedUI : UIManager
{

    GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();        
    }

    public void resume()
    {
        gm.setPaused(false);
        hideScreen(this.transform);
    }
}
