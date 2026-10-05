using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerLook : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform cameraPivot;

    [Header("Sensibilidad")]
    [SerializeField] private float sensitivityX = 0.1f;
    [SerializeField] private float sensitivityY = 0.1f;

    [Header("Límites verticales (grados)")]
    [SerializeField] private float minPitch = -85f; 
    [SerializeField] private float maxPitch = 60f;

    private float currentPitch = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        transform.Rotate(0f, mouseDelta.x * sensitivityX, 0f);

        currentPitch -= mouseDelta.y * sensitivityY;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
        cameraPivot.localRotation = Quaternion.Euler(currentPitch, 0f, 0f);
    }
}
