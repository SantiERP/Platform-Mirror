using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityLister
{
    static Transform TransformPlayer;
    static bool dadoVuelta;

    public static Transform PlayerT
    {
        get{return TransformPlayer;}
        set{TransformPlayer = value;}
    }

    public static bool DadoVuelta
    {
        get{return dadoVuelta;}
        set{dadoVuelta = value;}
    }
}
