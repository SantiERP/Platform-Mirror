using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bouncing : MonoBehaviour
{
    Rigidbody rb;

    public Rigidbody rig
    {
        get
        {
            return rb;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

}
