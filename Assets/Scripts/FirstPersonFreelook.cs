using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonFreelook : MonoBehaviour
{
    [Header("Camera")]
    [Tooltip("Vertical camera sensitivity")]
    public float sensitivity_horizontal = 100;
    [Tooltip("Horizontal camera sensitivity")]
    public float sensitivity_vertical = 100;
    [Tooltip("Vertical camera max up angle")]
    public float vertical_max = 80f;
    [Tooltip("Vertical camera max down angle")]
    public float vertical_min = -80f;
    //In here we will store the 3 angle axis of the camera
    private float yaw = 0;
    private float pitch = 0;
    private float roll = 0;

    [Header("Movement")]
    [Tooltip("Movement speed")]
    public float speed_base = 10;

    //We reserve the required memory to use the input manager
    InputManager input;

    //The start function runs only once when the object appears on the scene
    private void Start()
    {
        //We save the initial camera rotation
        Vector3 rotation = transform.localEulerAngles;
        yaw = rotation.y;
        pitch = rotation.x;
        roll = rotation.z;
        //We hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        //We create the input manager
        input = new InputManager();
        //We enable the inputs related to the gameplay
        input.Player.Enable();
        Debug.Log("This is a console info message");
        Debug.LogWarning("This is a console warning message");
        Debug.LogError("This is a console error message");
        Debug.LogException(new Exception("An exception has been thrown"));
    }

    //The update funcion runs once per frame. You can check the duration in seconds of the previous frame using Time.deltaTime
    private void Update()
    {
        //We read the "move" input action from the InputManager. This action stores a Vector2 value, two decimals x and y
        Vector2 move = input.Player.Move.ReadValue<Vector2>();
        //We multiply the raw input value with a decimal number to control the speed of movement
        move = move * speed_base;
        //We multiply the input value with Time.deltaTime to make it run at the same speed on any computer
        move = move * Time.deltaTime;
        //We use the y value that stores if the w or s key are pressed to move the camera along its forward direction
        transform.position = transform.position + transform.forward * move.y;
        //We use the x value that stores if the a or d key are pressed to move the camera along its right direction
        transform.position = transform.position + transform.right * move.x;

        //We read the "look" input action from the InputManager. This action stores a Vector2 value, two decimals x and y
        Vector2 look = input.Player.Look.ReadValue<Vector2>();
        //We multiply the raw input value with a decimal number to control the speed of movement.
        //Because the mouse movement already has the Time.deltaTime integrated, there is no need to multiply it this time
        look.x = look.x * sensitivity_horizontal;
        look.y = look.y * sensitivity_vertical;
        yaw = yaw + look.x;
        pitch = pitch - look.y;
        //We limit the pitch angle with the minimum and maximum values
        pitch = Mathf.Clamp(pitch, vertical_min, vertical_max);
        //We apply the new angles to the camera
        transform.localEulerAngles = new Vector3(pitch, yaw, roll);
    }
}
