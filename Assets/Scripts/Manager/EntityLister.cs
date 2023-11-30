using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityLister
{
    static Transform _playerT;
    static bool _dadoVuelta;

    public static Transform PlayerT
    {
        get{return _playerT;}
        set{_playerT = value;}
    }

    public static bool DadoVuelta
    {
        get{return _dadoVuelta;}
        set{_dadoVuelta = value;}
    }
}
