using System.Collections;
using UnityEngine;

public class Button : MonoBehaviour
{
    public delegate void delegatebutton();

    [SerializeField] float _speedDown;
    float _modPosEnd;

    Vector3 posin, posend;

    private void Start()
    {
        _modPosEnd=transform.position.y-0.3f;
        posin = transform.position;
        posend = Vector3.up * _modPosEnd*-1f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing j))
        {
            StartCoroutine("Button Down");
        }        
    }
    IEnumerator buttondown()
    {
        while(transform.position.y!=_modPosEnd) 
        {
            transform.position += posend * _speedDown * Time.deltaTime;
        }

        yield return null;
    }
}
