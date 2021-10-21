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
        transform.gameObject.SetActive(false);
        objectController.collect(transform.gameObject);
        gm.timerInSeconds += 30;
    }
}
