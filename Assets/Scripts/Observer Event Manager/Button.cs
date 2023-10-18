using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public delegate void delegatebutton();

    [SerializeField] float _speeddown;
    float _modposend;

    Vector3 posin, posend;

    private void Start()
    {
        _modposend=transform.position.y-0.3f;
        posin = transform.position;
        posend = Vector3.up * _modposend*-1f;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing j))
        {
            StartCoroutine("buttondown");
        }        
    }
    IEnumerator buttondown()
    {
        while(transform.position.y!=_modposend) 
        {
            transform.position += posend * _speeddown * Time.deltaTime;
        }

        yield return null;
    }
}
