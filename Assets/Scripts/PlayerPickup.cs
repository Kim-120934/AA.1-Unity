using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [Header("Detección")]
    [SerializeField] private LayerMask pickupLayers;          
    [SerializeField] private float pickupDistance = 2f;       

    private Rigidbody heldBody;      
    private float heldDistance;      

    private void Update()
    {
        bool mousePressed = Mouse.current.leftButton.isPressed;

        
        Debug.DrawRay(transform.position, transform.forward * pickupDistance,
                      heldBody == null ? Color.yellow : Color.green);

        if (mousePressed)
        {
           
            if (heldBody == null)
            {
                TryPickUp();
            }
        }
        else if (heldBody != null)
        {
            Release();
        }
    }

    private void LateUpdate()
    {
        
        if (heldBody != null)
        {
            heldBody.transform.position = transform.position + transform.forward * heldDistance;
        }
    }

    private void TryPickUp()
    {
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(transform.position, transform.forward, out hit,
                                            pickupDistance, pickupLayers,
                                            QueryTriggerInteraction.Ignore);

       
        if (hitSomething && hit.rigidbody != null)
        {
            heldBody = hit.rigidbody;
            heldDistance = Vector3.Distance(transform.position, heldBody.transform.position);

            heldBody.linearVelocity = Vector3.zero;
            heldBody.angularVelocity = Vector3.zero;
            heldBody.isKinematic = true; 
        }
    }

    private void Release()
    {
        heldBody.isKinematic = false; 
        heldBody = null;              
    }
}
