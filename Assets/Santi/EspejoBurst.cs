using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspejoBurst : Espejos
{

    public override void Skill(Personaje personaje)
    {
        personaje.rig.velocity *= 2; 
    }

}
