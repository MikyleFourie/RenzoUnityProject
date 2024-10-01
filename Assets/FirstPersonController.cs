using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 1500f;

    private float xRotation = 0f;
    private CharacterController controller;

    float ySpeed;
    float gravity = 0.1f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;  // Locks the cursor to the center of the screen
    }

    void Update()
    {
        // Get mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Prevent over-rotation
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Get movement input for WASD keys
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //// Translate input into world space
        Vector3 move = transform.right * moveX * speed + transform.forward * moveZ * speed;

        // Check for right mouse button click
        if (Input.GetMouseButton(1)) // 1 is the right mouse button
        {
            // Move forward
            move = transform.forward * speed;

            //gravity
            ySpeed -= gravity * Time.deltaTime;
            move.y = ySpeed;

            // Move the character
            controller.Move(move * Time.deltaTime);
        }
        else
        {
            // Reset vertical speed when not moving
            ySpeed = 0;
        }


        //gravity
        ySpeed -= gravity * Time.deltaTime;
        move.y = ySpeed;

        // Move the character
        controller.Move(move * Time.deltaTime);
    }
}

