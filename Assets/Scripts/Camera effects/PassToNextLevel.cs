using UnityEngine;

public class PassToNextLevel : MonoBehaviour
{
        delegate void Actividad();

        Actividad _go;
        Actividad _goBack;

        Actividad _actual;

        void Start()
        {
             _go = CameraManager.Instance.NextPosition;
             _goBack = CameraManager.Instance.BeforePosition;
             _actual = _go;
        }

    void OnTriggerExit(Collider collider)
    {
        Player character;

        if(collider.TryGetComponent<Player>(out character))        
        {
            SaveManager.Save();
            _actual();

            if(_actual == _go)
            {
                _actual = _goBack;
            }
            else
            {
                _actual = _go;
            }
        }
    }
}
