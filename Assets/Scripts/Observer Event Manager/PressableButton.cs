using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableButton : Interruptor
{
    Vector3 desiredPosition;
    Vector3 normalPosition;

    void Start()
    {
        normalPosition = transform.position;
        desiredPosition = normalPosition;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.01f);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing rebotable))
        {
            ButtonAction();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing rebotable))
        {
            ButtonAntiAction();
        }
    }

    public override void NormalAction()
    {
        Debug.Log("Accion");
        desiredPosition = normalPosition - transform.up * 0.8f;
    }

    public override void AntiAction()
    {
        Debug.Log("AntiAccion");
        desiredPosition = normalPosition;
    }

}
