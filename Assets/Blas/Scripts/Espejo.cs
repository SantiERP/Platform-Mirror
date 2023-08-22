using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espejo : MonoBehaviour
{
    void OnTriggerEnter(Collider colision)
    {
        Personaje personaje = colision.GetComponent<Personaje>();
        if(personaje != null)
        {
            Rigidbody rigPersonaje = personaje.GetComponent<Rigidbody>();

            rigPersonaje.velocity = Vector3.Reflect(rigPersonaje.velocity, transform.up);
        }
    }
}
