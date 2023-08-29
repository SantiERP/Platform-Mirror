using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspejoAntigravedad : Espejos
{
    bool _empujearriba;
    Rigidbody rb;
    [Header("Fuerza de empuje")]
    [SerializeField,Range(0,100)] float _fuerzadeempuje;
    [SerializeField] ForceMode _forceMode;

    [SerializeField] float speed;
    float timeCount = 0.0f;


    private void Update()
    {
        if(_empujearriba)
        {
            Quaternion _rotation = new Quaternion (0,0,180,0);
            rb.AddForce(Vector3.up * _fuerzadeempuje);
            EntityLister.JugadorT.rotation = Quaternion.Slerp(transform.rotation, _rotation, speed * timeCount);
            timeCount = timeCount + Time.deltaTime;
            /*
            if (rb.velocity.x < 0.9f)
            {
                Debug.Log("deja de empujar");
                _empujearriba = false;
            }
            */
        }
    }
    public override void Skill(Rigidbody r)
    {
        rb = r;
        _empujearriba=true;
    }
}
