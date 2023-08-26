using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspejoAntigravedad : Espejos
{
    public override void Skill(Personaje personaje)
    {
        Debug.Log("entra");
        personaje.rig.AddForce(transform.up * 300);
    }
}
