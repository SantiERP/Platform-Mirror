using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosCamaras : MonoBehaviour
{
    [SerializeField] float tamaño;
    [SerializeField][Range (0.1f , 10)] float tiempoDeTrancision;
    [SerializeField] float esperaEntreMomentos = 0.01f;
    [SerializeField] AnimationCurve Curva;

    public void SetCamera()
    {
        StartCoroutine(ActualSetting());
    }

    IEnumerator ActualSetting()
    {
        Camera MainCamera = Camera.main;
        WaitForSeconds Espera = new WaitForSeconds(esperaEntreMomentos);
        float relacionDeTiempo = 1/tiempoDeTrancision;
        
        Vector3 posInicial = MainCamera.transform.position;
        float tamañoInicial = MainCamera.orthographicSize;

        for(float i = 0; i < tiempoDeTrancision; i += esperaEntreMomentos)
        {
            float puntoEnLaCurva = Curva.Evaluate(i * relacionDeTiempo);

            MainCamera.transform.position = Vector3.Lerp(posInicial , transform.position , puntoEnLaCurva);
            MainCamera.orthographicSize = Mathf.Lerp(tamañoInicial , tamaño , puntoEnLaCurva);

            yield return Espera;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.forward * 10, Vector3.forward * 20 + Vector3.up * tamaño*2 + Vector3.right * Camera.main.aspect * tamaño*2);
    }
}
