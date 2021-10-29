using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperty : MonoBehaviour
{
    public float secondsToAdd = 30;
    GameManager gm;
    ObjectController objectController;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        objectController = gm.GetComponent<ObjectController>();
    }

    public void OnCollect()
    {
        objectController.collect();
        GameObject objectListUI = GameObject.Find("ObjectList");
        objectListUI.transform.Find(transform.name + "_" + transform.parent.name + "_text").gameObject.SetActive(false);
        transform.gameObject.SetActive(false);
        gm.timerInSeconds += 30;
    }
}
