using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Interruptor : MonoBehaviour
{
    public delegate void Action();
    public Action ButtonAction;
    public Action ButtonAntiAction;




    // Start is called before the first frame update
    void Awake()
    {
        ButtonAction += NormalAction;
        ButtonAntiAction += AntiAction;
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entra");

        if (other.TryGetComponent<Rebotable>(out Rebotable rebotable))
        {
            Debug.Log("Rebotable");
            NormalAction();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Rebotable>(out Rebotable rebotable))
        {
            AntiAction();
        }
    }

    public abstract void NormalAction();

    public abstract void AntiAction();


}
