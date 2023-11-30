using System.Collections;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public delegate void Delegatebutton();

    [SerializeField] float _speedDown;
    float _modPosEnd;

    Vector3 _posend;

    private void Start()
    {
        _modPosEnd=transform.position.y-0.3f;
        _posend = Vector3.up * _modPosEnd*-1f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing j))
        {
            StartCoroutine("Button Down");
        }        
    }
    IEnumerator Buttondown()
    {
        while(transform.position.y!=_modPosEnd) 
        {
            transform.position += _posend * _speedDown * Time.deltaTime;
        }

        yield return null;
    }
}
