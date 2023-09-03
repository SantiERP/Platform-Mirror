using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje : Rebotable
{
    public virtual void NormalMove(float horizontal, float vertical, bool enElAire, float velocidadenAire, float MaxVelocidadHorizontal, AnimationCurve Aceleracion)
    {

    }

    public void Throw(Vector3 direction)
    {
        rig.velocity += direction * 2;
    }
}
