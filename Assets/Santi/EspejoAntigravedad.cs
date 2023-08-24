using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspejoAntigravedad : Espejos
{
    public override void Skill(Rigidbody rig)
    {
        Debug.Log("entra");
        rig.AddForce(transform.up * 300);
    }
}
