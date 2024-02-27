using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPan : MonoBehaviour
{
    delegate void Actividad();

    Actividad _go;
    Actividad _goBack;

    Actividad _actual;
    bool passedThroughHere = false;

    void Start()
    {
        _go = CameraManager.Instance.NextPosition;
        _goBack = CameraManager.Instance.BeforePosition;
        _actual = _go;
    }

    void OnTriggerExit(Collider collider)
    {
        Player character;
        if (!passedThroughHere)
            SaveManager.Save(); passedThroughHere = true;

        if (collider.TryGetComponent<Player>(out character))
        {
            _actual();

            if (_actual == _go)
            {
                _actual = _goBack;
                Destroy(gameObject);
            }
            else
            {
                _actual = _go;
            }
        }
    }
}
