using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Bouncing
{
    public virtual void NormalMove(float horizontal, float vertical)
    {

    }

    public void Throw(Vector3 direction)
    {
        rig.velocity += direction * 2;
    }
}
