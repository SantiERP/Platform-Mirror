using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Espejos : MonoBehaviour 
{
    public abstract void Skill( Rigidbody rig);

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rebotable>(out Rebotable rebotable))
        {
            Skill(rebotable.rig);
        }
    }

}
