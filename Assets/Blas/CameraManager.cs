using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] PuntosCamaras[] puntosCamaras;
    public int posActual = 0;

    void Awake()
    {
        instance = this;
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

    public void Load(int punto, Vector3 pos)
    {
        puntosCamaras[0].StopAllCoroutines();

        posActual = punto;
        transform.position = pos;
    }
}
