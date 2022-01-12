using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperty : MonoBehaviour
{
    public float secondsToRemove = 5;
    GameManager gm;
    ObjectController objectController;

    public GameObject textObject;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        objectController = gm.GetComponent<ObjectController>();
    }

    public void OnCollect()
    {
        GameObject objectListUI = GameObject.Find("ObjectList");
        textObject.SetActive(false);
        transform.gameObject.SetActive(false);
        objectController.collect();
        gm.timeInSeconds -= secondsToRemove;
    }
}
