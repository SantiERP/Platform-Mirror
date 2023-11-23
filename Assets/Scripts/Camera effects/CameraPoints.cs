using System.Collections;
using UnityEngine;

public class CameraPoints : MonoBehaviour
{
    [SerializeField] float _size;
    [SerializeField][Range (0.1f , 10)] float _TRANSITION_TIME = 1.2f;
    [SerializeField] float _waitBetweeMoments = 0.01f;
    [SerializeField] AnimationCurve _curve;

    public void SetCamera(int dir)
    {
        StartCoroutine(ActualSetting(dir));
    }

    IEnumerator ActualSetting(int dir)
    {
        Camera MainCamera = Camera.main;
        WaitForSeconds wait = new WaitForSeconds(_waitBetweeMoments);

        float timeRalation = 1/_TRANSITION_TIME;
        
        Vector3 initialPos = MainCamera.transform.position;
        Vector3 finalPos = transform.position;

        finalPos.z = transform.position.z - (_size * Mathf.Tan(90 - (MainCamera.fieldOfView/2))) - 5;

        float initialSize = MainCamera.orthographicSize;

        Transform Player = EntityLister.PlayerT;
        if (Player) Debug.Log("no nulo");
        ModelPlayer Model = Player.gameObject.GetComponent<ModelPlayer>();
        Model.enabled = false;
        Rigidbody rigPlayer = Player.GetComponent<Rigidbody>();
        rigPlayer.velocity = MainCamera.transform.right * dir;

        for (float i = 0; i < _TRANSITION_TIME; i += _waitBetweeMoments)
        {
            float pointInTheCurve = _curve.Evaluate(i * timeRalation);

            MainCamera.transform.position = Vector3.Lerp(initialPos , finalPos, pointInTheCurve);
            MainCamera.orthographicSize = Mathf.Lerp(initialSize , _size , pointInTheCurve);

            yield return wait;
        }

        Model.enabled = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + Vector3.forward * 10, Vector3.forward * 20 + Vector3.up * _size*2 + Vector3.right * Camera.main.aspect * _size*2);
    }
}
