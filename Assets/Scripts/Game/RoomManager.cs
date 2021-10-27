using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    Controller player;

    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player") player.currentRoom = transform.name;
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player") player.currentRoom = "None";
    }
}
