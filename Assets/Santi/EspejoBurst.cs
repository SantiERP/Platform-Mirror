using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspejoBurst : Espejos
{

    public override void Skill(Rigidbody rig)
    {
        rig.velocity *= 2; 
    }

}
