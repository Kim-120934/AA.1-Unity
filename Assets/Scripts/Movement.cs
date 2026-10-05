using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("hola mundo");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(0, 0, 0);

        if (Keyboard.current.wKey.isPressed)
        {
            dir.z = 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            dir.z = -1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            dir.x = 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            dir.x = -1;
        }

        dir= transform.TransformDirection(dir);

        transform.position = transform.position + dir * speed * Time.deltaTime;
    }
}
