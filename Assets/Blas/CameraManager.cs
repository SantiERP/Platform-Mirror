using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] PuntosCamaras[] puntosCamaras;
    int posActual = 0;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        puntosCamaras[0].SetCamera();
    }

    public void NextPosition()
    {
        posActual++;
        puntosCamaras[posActual].SetCamera();
    }

    public void BeforePosition()
    {
        posActual--;
        puntosCamaras[posActual].SetCamera();
    }
}
