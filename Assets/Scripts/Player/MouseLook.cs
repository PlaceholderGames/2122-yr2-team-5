using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100;
    
    Transform playerBody;
    float xRotation = 0;

    Settings gs;
    GameManager gm;

    bool invertLookX = false, invertLookY = false;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;

        GameObject gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    bool intToBool(int i)
    {
        return i == 1 ? true : false;
    }

    void Update()
    {
        invertLookX = intToBool(PlayerPrefs.GetInt("mouse.invert.x"));
        invertLookY = intToBool(PlayerPrefs.GetInt("mouse.invert.y"));
        mouseSensitivity = PlayerPrefs.GetFloat("mouse.sensitivity");

        Cursor.visible = gm.isPaused() || gm.isGameOver();
        if(gm.isPaused() || gm.isGameOver()) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;


        if (!gm.isGameOver())
        {
            // Mouse Look
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            mouseX = invertLookX ? -mouseX : mouseX;
            mouseY = invertLookY ? -mouseY : mouseY;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up, mouseX);
        }
    }
}