using UnityEngine;

public class AdvancedMovement : MonoBehaviour
{
    public float speed = 1;
    public Vector2 direction;   
    public bool isGrounded;     
    public float jumpForce = 6;

    InputSystem_Actions inputs;
    Rigidbody rb;

    private void Start()
    {
        inputs = new InputSystem_Actions();
        inputs.Enable();

        rb = GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        inputs.Disable();
    }

    void Update()
    {
        Vector2 input = inputs.Player.Move.ReadValue<Vector2>();

        direction = input;

        Vector3 dir = input;
        dir.z = dir.y;
        dir.y = 0;

        dir.x *= 0.75f;
        if (dir.z < 0) dir.z *= 0.5f;

        dir = transform.TransformDirection(dir);

        
        isGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1.1f);

        float currentSpeed = speed;

        if (!isGrounded)
        {
            currentSpeed *= 0.5f;
        }
        else if (inputs.Player.Sprint.IsPressed() && input.y > 0)
        {
            currentSpeed *= 2f;

           
        }

        transform.position = transform.position + dir * currentSpeed * Time.deltaTime;

        if (isGrounded && inputs.Player.Jump.WasPressedThisFrame() && rb.linearVelocity.y <= 0.1f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
}