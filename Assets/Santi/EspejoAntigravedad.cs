using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspejoAntigravedad : Espejos
{
    public override void Skill(Rigidbody r)
    {
        Debug.Log("entra");
        r.AddForce(transform.up * 300);
    }
}
