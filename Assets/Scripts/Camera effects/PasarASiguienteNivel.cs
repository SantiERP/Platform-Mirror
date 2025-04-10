using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarASiguienteNivel : MonoBehaviour
{
        delegate void Actividad();

        Actividad ir;
        Actividad volver;

        Actividad actual;

        void Start()
        {
             ir = CameraManager.instance.NextPosition;
             volver = CameraManager.instance.BeforePosition;
             actual = ir;
        }

    void OnTriggerEnter(Collider collider)
    {
        Player personaje;

        if(collider.TryGetComponent<Player>(out personaje))        
        {
            SaveManager.Save();
            actual();

            if(actual == ir)
            {
                
                actual = volver;
            }
            else
            {
                actual = ir;
            }
        }
    }
}
