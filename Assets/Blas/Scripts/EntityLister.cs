using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityLister
{
    static Transform TransformJugador;

    public static Transform JugadorT
    {
        get{return TransformJugador;}
        set{TransformJugador = value;}
    }
}
