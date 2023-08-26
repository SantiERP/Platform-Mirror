using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Espejos : MonoBehaviour 
{
    public abstract void Skill( Personaje per);

    public void OnTriggerEnter(Collider other)
    {
        Personaje personaje = other.GetComponent<Personaje>();

        if (personaje != null)
        {
            Skill(personaje);
        }
    }

}
