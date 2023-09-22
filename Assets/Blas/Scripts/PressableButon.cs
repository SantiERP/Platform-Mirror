using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PressableButon : MonoBehaviour
{
    public delegate void Action();
    public Action ButtonAction;
    public Action ButtonAntiAction;
    Vector3 desiredPosition;
    Vector3 normalPosition;

    // Start is called before the first frame update
    void Start()
    {
        ButtonAction += NormalAction;
        ButtonAntiAction += AntiAction;
        normalPosition = transform.position;
        desiredPosition = normalPosition;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position , desiredPosition , 0.01f);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        ButtonAction();
    }

    void OnCollisionExit(Collision collision)
    {
        ButtonAntiAction();
    }

    void NormalAction()
    {
        Debug.Log("Accion");
        desiredPosition = normalPosition - transform.up * 0.8f;
    }

    void AntiAction()
    {
        Debug.Log("AntiAccion");
        desiredPosition = normalPosition;
    }


}
