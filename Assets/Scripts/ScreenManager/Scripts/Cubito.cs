using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubito : MonoBehaviour
{
    public bool testCoroutine;

    void Start()
    {
        if (testCoroutine)
            StartCoroutine(PauseTest());
    }

    private void OnEnable()
    {
        //Para el rigidbody podemos devolverle al rgbd.velocity el vector3 que habiamos guardado en el OnDisable
    }

    private void OnDisable()
    {
        //Para el rigidbody podemos guardar el velocity en un vector3, ponerlo en 0 el rgbd.velocity
    }

    void Update()
    {
        transform.position -= Vector3.up * (2f * Time.deltaTime);
    }

    IEnumerator PauseTest()
    {
        float acum = 0;

        while (true)
        {
            acum++;
            Debug.Log(acum);

            //Esperar hasta que esto este prendido
            //Esto:
            //yield return new WaitUntil(IsEnabled);
            //Es lo mismo que esto: 
            yield return new WaitUntil(() => enabled);
        }
    }

    bool IsEnabled()
    {
        return enabled;
    }
}
