using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class ObjectCollector : MonoBehaviour
{
    Controller playerController;
    GameManager gameManager;
    ObjectController objectController;

    GameObject gm;


    // Interactivity
    RaycastHit hit;
    public string objectTag;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<Controller>();
        gm = GameObject.Find("GameManager");

        gameManager = gm.GetComponent<GameManager>();
        objectController = gm.GetComponent<ObjectController>();


        gameManager.showCollectUI(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, gameManager.raycastDistance, (1 << 7))) {
            if(hit.transform.gameObject.tag == objectTag)
            {
                gameManager.showCollectUI(true);
                if(Input.GetKeyDown(playerController.interactKey))
                {
                    hit.transform.GetComponent<ObjectProperty>().OnCollect();
                }
            }
        } else {
            gameManager.showCollectUI(false);
        }
    }
}
