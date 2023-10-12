using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntigravityMirror : Mirrors
{
    [SerializeField] bool _empujearriba;
    Rigidbody rb;
    [Header("Fuerza de empuje")]
    [SerializeField,Range(0,100)] float _fuerzadeempuje;
    [SerializeField] ForceMode _forceMode;

    [SerializeField] float speed;

    public override void Skill(Rigidbody r)
    {     
        EntityLister.DadoVuelta = !EntityLister.DadoVuelta;


        Physics.gravity = transform.up * Physics.gravity.magnitude;

        StartCoroutine(Rotar((-transform.up)));
    }

    IEnumerator Rotar(Vector3 up)
    {
        //Quaternion rotacionInicial = EntityLister.JugadorT.rotation;

        EntityLister.JugadorT.up = up;

        yield return null;
        
        // while(timeCount < 1)
        // {
        //     EntityLister.JugadorT.rotation = Quaternion.Slerp(rotacionInicial, _rotation, timeCount);
        //     timeCount += 0.1f;
        //     yield return new WaitForSeconds(0.1f);
        // }
    }
}
