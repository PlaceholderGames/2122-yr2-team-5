using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    [Header("User control")]
    public KeyCode interactKey = KeyCode.E;

    CharacterController controller;

    [Header("Forces")]
    public float speed = 10f;

    Vector3 velocity;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Ground")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;
    bool isGrounded;

    public Vector3 lastPosition;

    public string currentRoom = "None";


    GameManager gm;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.isGameOver() && gm.completedTutorial && !gm.isPaused())
        {
            lastPosition = transform.position;

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            if (isGrounded & velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = (transform.right * x) + (transform.forward * z);

            if (Input.GetButton("Sprint"))
            {
                controller.Move(move * (speed * 1.5f) * Time.deltaTime);
            } else
            {
                controller.Move(move * speed * Time.deltaTime);
            }

            if ((Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}