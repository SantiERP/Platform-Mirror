using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Personaje : MonoBehaviour
{
    Rigidbody rb;    

    public Rigidbody rig
    {
        get{return rb;}
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Move(float horizontal, float vertical, bool enElAire)
    {

    }

    public void Throw(Vector3 direction)
    {
        rb.velocity += direction * 2;
    }
}
