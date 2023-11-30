using UnityEngine;

public class Player : Bouncing
{
    public virtual void NormalMove(float horizontal, float vertical)
    {

    }

    public void Throw(Vector3 direction)
    {
        Rig.velocity += direction * 2;
    }
}
