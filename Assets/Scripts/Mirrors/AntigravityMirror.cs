using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntigravityMirror : Mirrors
{
    [SerializeField] bool _upPush;
    Rigidbody rb;
    [Header("Push Force")]
    [SerializeField,Range(0,100)] float _pushForce;
    [SerializeField] ForceMode _forceMode;

    [SerializeField] float speed;

    public override void Skill(Rigidbody r)
    {     
        EntityLister.DadoVuelta = !EntityLister.DadoVuelta;


        Physics.gravity = transform.up * Physics.gravity.magnitude;

        StartCoroutine(Rotate((-transform.up)));
    }

    IEnumerator Rotate(Vector3 up)
    {
        EntityLister.PlayerT.up = up;

        yield return null;
    }
}
