using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator anim;
    public AdvancedMovement movement;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (movement == null) movement = GetComponentInParent<AdvancedMovement>();
    }

    void Update()
    {
        anim.SetFloat("Forward", movement.direction.y, 0.1f, Time.deltaTime);
        anim.SetFloat("Sideways", movement.direction.x, 0.1f, Time.deltaTime);

        
        anim.SetBool("Falling", !movement.isGrounded);
    }
}