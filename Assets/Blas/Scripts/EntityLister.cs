using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityLister
{
    static Transform TransformJugador;
    static bool dadoVuelta;

    public static Transform JugadorT
    {
        get{return TransformJugador;}
        set{TransformJugador = value;}
    }

    public static bool DadoVuelta
    {
        get{return dadoVuelta;}
        set{dadoVuelta = value;}
    }
}
