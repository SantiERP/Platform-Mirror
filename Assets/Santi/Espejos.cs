using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Espejos : MonoBehaviour 
{
    public abstract void Skill( Rigidbody rig);

    public void OnTriggerEnter(Collider other)
    {
        Rigidbody rig = other.GetComponent<Rigidbody>();

        if (other.gameObject.layer == 7)
        {
            Skill(rig);
        }
    }

}
