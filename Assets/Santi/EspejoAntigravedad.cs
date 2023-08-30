using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspejoAntigravedad : Espejos
{
    [SerializeField] bool _empujearriba;
    Rigidbody rb;
    [Header("Fuerza de empuje")]
    [SerializeField,Range(0,100)] float _fuerzadeempuje;
    [SerializeField] ForceMode _forceMode;

    [SerializeField] float speed;
    float timeCount = 0.0f;


    private void Update()
    {
        // if(EntityLister.DadoVuelta)
        // {
        //     Quaternion _rotation = new Quaternion (0,0,180,0);
        //     EntityLister.JugadorT.rotation = Quaternion.Slerp(transform.rotation, _rotation, speed * timeCount);
            
        //     timeCount += Time.deltaTime;
        // }

        // else
        // {
        //     Quaternion _rotation = new Quaternion (0,0,0,0);
        //     EntityLister.JugadorT.rotation = Quaternion.Slerp(transform.rotation, _rotation, speed * timeCount);
            
        //     timeCount += Time.deltaTime;
        // }

            
    }
    public override void Skill(Rigidbody r)
    {       
        EntityLister.DadoVuelta = !EntityLister.DadoVuelta;


        Physics.gravity = -Physics.gravity;

        if(EntityLister.DadoVuelta)
        {
            StartCoroutine(Rotar(new Quaternion (0,0,180,0)));
            
        }

        else
        {
            StartCoroutine(Rotar(new Quaternion (0,0,0,0)));
        }
    }

    IEnumerator Rotar(Quaternion _rotation)
    {
        timeCount = 0f;
        Quaternion rotacionInicial = EntityLister.JugadorT.rotation;

        EntityLister.JugadorT.rotation = _rotation;

        yield return null;
        
        // while(timeCount < 1)
        // {
        //     EntityLister.JugadorT.rotation = Quaternion.Slerp(rotacionInicial, _rotation, timeCount);
        //     timeCount += 0.1f;
        //     yield return new WaitForSeconds(0.1f);
        // }
    }
}
