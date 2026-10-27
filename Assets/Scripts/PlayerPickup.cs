using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    [Header("Resaltado")]
    [SerializeField] private Color highlightColor = Color.yellow;

    private Renderer currentTarget;
    private Color originalColor;

    [Header("Detección")]
    [SerializeField] private LayerMask pickupLayers;
    [SerializeField] private float pickupDistance = 2f;

    [Header("Distancia del objeto")]
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float scrollSensitivity = 0.5f;

    [Header("Fuerzas del objeto")]
    [SerializeField] private float springForce = 50f;
    [SerializeField] private float damping = 10f;

    [Header("Lanzamiento")]
    [SerializeField] private float throwForce = 5f;

    private Rigidbody heldBody;
    private float heldDistance;

    private void Update()
    {
        UpdateHighlight();
        if (Mouse.current == null)
            return;

        bool mousePressed = Mouse.current.leftButton.isPressed;

        Debug.DrawRay(
            transform.position,
            transform.forward * pickupDistance,
            heldBody == null ? Color.yellow : Color.green
        );

        if (mousePressed && heldBody == null)
        {
            TryPickUp();
        }
        else if (!mousePressed && heldBody != null)
        {
            Release();
        }

       
        if (heldBody != null)
        {
            float scroll = Mouse.current.scroll.ReadValue().y;

            heldDistance += scroll * scrollSensitivity * 0.01f;
            heldDistance = Mathf.Clamp(
                heldDistance,
                minDistance,
                maxDistance
            );
        }
    }

    private void FixedUpdate()
    {
        if (heldBody == null)
            return;

        
        Vector3 targetPosition =
            transform.position + transform.forward * heldDistance;

        
        Vector3 displacement = targetPosition - heldBody.position;

        Vector3 force =
            displacement * springForce
            - heldBody.linearVelocity * damping;

        heldBody.AddForce(force, ForceMode.Acceleration);
    }

    private void TryPickUp()
    {
        if (heldBody != null)
            return;

        bool hitSomething = Physics.Raycast(
            transform.position,
            transform.forward,
            out RaycastHit hit,
            pickupDistance,
            pickupLayers,
            QueryTriggerInteraction.Ignore
        );

        if (hitSomething && hit.rigidbody != null)
        {
            heldBody = hit.rigidbody;

            heldDistance = Mathf.Clamp(
                Vector3.Distance(
                    transform.position,
                    heldBody.position
                ),
                minDistance,
                maxDistance
            );

            
            heldBody.isKinematic = false;
            heldBody.collisionDetectionMode =
                CollisionDetectionMode.ContinuousDynamic;
        }
    }

    private void Release()
    {
        if (heldBody == null)
            return;

       
        heldBody.AddForce(
            transform.forward * throwForce,
            ForceMode.Impulse
        );

        heldBody = null;
    }
    private void UpdateHighlight()
    {
        RaycastHit hit;

        bool canPickup = Physics.Raycast(
            transform.position,
            transform.forward,
            out hit,
            pickupDistance,
            pickupLayers,
            QueryTriggerInteraction.Ignore
        ) && hit.rigidbody != null;

        Renderer newTarget = canPickup
            ? hit.rigidbody.GetComponentInChildren<Renderer>()
            : null;

        if (newTarget == currentTarget)
            return;

        // Restaurar el color del objeto anterior.
        if (currentTarget != null)
            currentTarget.material.color = originalColor;

        currentTarget = newTarget;

        // Resaltar el nuevo objeto.
        if (currentTarget != null)
        {
            originalColor = currentTarget.material.color;
            currentTarget.material.color = highlightColor;
        }
    }
}