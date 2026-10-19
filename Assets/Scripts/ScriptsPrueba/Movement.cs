using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 1;

    InputSystem_Actions inputs;

    private void Start()
    {
        inputs = new InputSystem_Actions();
        inputs.Enable();

    }

    void Update()
    {
        Debug.Log(inputs.Player.Move.ReadValue<Vector2>());


        Vector3 dir = inputs.Player.Move.ReadValue<Vector2>();

        dir.z = dir.y;

        dir.y = 0;

        dir = transform.TransformDirection(dir);

        transform.position = transform.position + dir * speed * Time.deltaTime;
    }
}
