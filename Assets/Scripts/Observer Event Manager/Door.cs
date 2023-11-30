using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Interruptor _activationButton;
    [SerializeField] Vector3 _whereDoIMoveTo;
    Vector3 _desiredPosition;
    Vector3 _normalPosition;

    [SerializeField] AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _normalPosition = transform.position;
        _desiredPosition = _normalPosition;
        _activationButton.ButtonAction += WhatToDo;
        _activationButton.ButtonAntiAction += WhatToDont;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _desiredPosition, 0.05f);
    }

    void WhatToDo()
    {
        _audioSource.Play();
        _desiredPosition = _whereDoIMoveTo + _normalPosition;
        Debug.Log("Moving");
    }

    void WhatToDont()
    {
        _desiredPosition = _normalPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position , transform.position + _whereDoIMoveTo);
    }
}
