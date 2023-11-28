using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Interruptor ActivationButton;
    [SerializeField] Vector3 WhereDoIMoveTo;
    Vector3 desiredPosition;
    Vector3 normalPosition;

    [SerializeField] AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        normalPosition = transform.position;
        desiredPosition = normalPosition;
        ActivationButton.ButtonAction += WhatToDo;
        ActivationButton.ButtonAntiAction += WhatToDont;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.05f);
    }

    void WhatToDo()
    {
        _audioSource.Play();
        desiredPosition = WhereDoIMoveTo + normalPosition;
        Debug.Log("Moving");
    }

    void WhatToDont()
    {
        desiredPosition = normalPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position , transform.position + WhereDoIMoveTo);
    }
}
