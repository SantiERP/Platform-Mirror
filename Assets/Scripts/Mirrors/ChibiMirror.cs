using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChibiMirror : Mirrors
{
    public override void Skill(Bouncing b)
    {
        ModelPlayer model = b.GetComponent<ModelPlayer>();
        VisualPlayer visualPlayer = b.GetComponentInChildren<VisualPlayer>();

        if (b.Small)
        {
            b.transform.localScale *= 2;
            if (model) 
            { 
                model.JumpStrenght = model.NormalJump;
                visualPlayer.Jump = delegate { visualPlayer.VisualJump(); };
                model.GravityMultiplier = 1;
            }
        }
        else
        {
            b.transform.localScale *= 0.5f;
            if (model) 
            { 
                model.JumpStrenght = 0;
                model.GravityMultiplier = 0.5f;
                visualPlayer.Jump = delegate { visualPlayer.TinyJump(); };
            }
        }
        b.Small = !b.Small;
    }

}
