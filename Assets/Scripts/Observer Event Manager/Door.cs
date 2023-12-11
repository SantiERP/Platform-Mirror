using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Interruptor _activationButton;
    [SerializeField] Vector3 _whereDoIMoveTo;
    [SerializeField] float _doorVelocity = 0.05f;
    Vector3 _desiredPosition;
    Vector3 _normalPosition;
    Rigidbody _rig;
    [SerializeField] AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _normalPosition = transform.position;
        _desiredPosition = _normalPosition;
        _activationButton.ButtonAction += WhatToDo;
        _activationButton.ButtonAntiAction += WhatToDont;
        //_rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position=Vector3.Lerp(transform.position, _desiredPosition, _doorVelocity);

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
