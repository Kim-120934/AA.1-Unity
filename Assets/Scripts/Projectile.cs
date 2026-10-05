using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    //How fast the object will launch
    public float force = 100;
    //We reserve the required memory to use the rigidbody
    Rigidbody rb;
    void Start()
    {
        //We search for the RigidBody component on the current object (the projectile)
        rb = GetComponent<Rigidbody>();
        //Using the rigidbody we add a force on its forward direction
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
