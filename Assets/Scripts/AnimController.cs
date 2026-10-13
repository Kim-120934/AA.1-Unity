using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator anim;
    public float Forward;
    public float Sideways;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Forward", Forward);
        anim.SetFloat("Sideways", Sideways);

    }
}
