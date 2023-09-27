using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Espejos : MonoBehaviour 
{
    [SerializeField] AnimationCurve entradaDePersonaje;

    public abstract void Skill( Rigidbody rig);

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Rebotable>(out Rebotable rebotable))
        {
            StartCoroutine(EntradaPersonaje(rebotable.transform.position, rebotable.rig));
        }
    }

    IEnumerator EntradaPersonaje(Vector3 posicionInicial, Rigidbody rig)
    {
        Vector3 dir = rig.velocity;
        Debug.Log($"<color=red>{dir}</color>");
        BoxCollider box = rig.GetComponent<BoxCollider>();  

        box.enabled = false;


        float instanteActual = 0f;
        Vector3 posicionFinal = posicionInicial - transform.up;

        rig.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;;

        while (instanteActual < 1)
        {
            rig.transform.position = Vector3.LerpUnclamped(posicionInicial, posicionFinal, entradaDePersonaje.Evaluate(instanteActual));

            instanteActual += 0.01f;

            yield return new WaitForSeconds(0.01f);
        }

        rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        box.enabled = true;        
        Skill(rig);
        Throw(dir, rig);
    }

    void Throw(Vector3 dir, Rigidbody r)
    {
        r.velocity = Vector3.Reflect(dir, transform.up);//Normalizar el vector
        //r.velocity *= 10;
        //r.velocity += transform.right;

        //r.AddForce(-Physics.gravity);
        Debug.Log($"<color=blue>{r.velocity}</color>");
        //Debug.DrawRay(r.transform.position, Vector3.Reflect(r.velocity + Physics.gravity * Time.fixedDeltaTime, transform.up), Color.red);
    }
}
