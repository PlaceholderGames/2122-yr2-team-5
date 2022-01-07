using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

public class ObjectCollector : MonoBehaviour
{
    PlayerController playerController;
    GameManager gameManager;
    ObjectController objectController;

    GameObject gm;

    public LayerMask layerMask;

    // Interactivity
    RaycastHit hit;
    public string objectTag;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        gm = GameObject.Find("GameManager");

        gameManager = gm.GetComponent<GameManager>();
        objectController = gm.GetComponent<ObjectController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, gameManager.raycastDistance, layerMask, QueryTriggerInteraction.Ignore))
        {
            bool canCollect = objectController.Find(hit.transform.gameObject);
            gameManager.showCollectUI(canCollect);
            
            if (canCollect)
            {
                gameManager.showCollectUIAtTransform(canCollect, hit.transform);
                if (Input.GetKeyDown(playerController.interactKey))
                {
                    hit.transform.GetComponent<ObjectProperty>().OnCollect();
                }
            }
        }
        else
        {
            gameManager.showCollectUI(false);
        }
    }
}
