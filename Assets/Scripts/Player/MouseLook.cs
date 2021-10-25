using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100;
    
    Transform playerBody;
    float xRotation = 0;

    Settings gameSettings;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;

        if(GameObject.Find("GameSettings"))
        {
            gameSettings = GameObject.Find("GameSettings").GetComponent<Settings>();
        }

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseSensitivity = gameSettings ? gameSettings.mouseSensitivity : 100;

        Cursor.visible = gm.isPaused();
        if(gm.isPaused()) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;


        if (!gm.isGameOver())
        {
            // Mouse Look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up, mouseX);
        }
    }
}