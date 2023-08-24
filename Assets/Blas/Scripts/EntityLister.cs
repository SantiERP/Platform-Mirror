using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityLister
{
    static Transform TransformJugador;

    public static Transform Jugador
    {
        get{return TransformJugador;}
        set{TransformJugador = value;}
    }
}
