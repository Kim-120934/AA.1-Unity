using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerLook : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform cameraPivot;

    [Header("Sensibilidad ratón")]
    [SerializeField] private float sensitivityX = 0.1f;
    [SerializeField] private float sensitivityY = 0.1f;

    [Header("Sensibilidad mando")]
    [SerializeField] private float gamepadSensitivityX = 180f;
    [SerializeField] private float gamepadSensitivityY = 120f;

    [Header("Límites verticales (grados)")]
    [SerializeField] private float minPitch = -85f;
    [SerializeField] private float maxPitch = 60f;

    private float currentPitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 lookInput = Vector2.zero;

       
        if (Mouse.current != null)
        {
            lookInput += Mouse.current.delta.ReadValue()
                * new Vector2(sensitivityX, sensitivityY);
        }

        if (Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.rightStick.ReadValue();

            lookInput.x += stick.x * gamepadSensitivityX * Time.deltaTime;
            lookInput.y += stick.y * gamepadSensitivityY * Time.deltaTime;
        }

       
        transform.Rotate(0f, lookInput.x, 0f);

        currentPitch -= lookInput.y;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        if (cameraPivot != null)
        {
            cameraPivot.localRotation =
                Quaternion.Euler(currentPitch, 0f, 0f);
        }
    }
}