using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorSanti : Personaje
{
    [Header("Stats Movimiento")]
    [SerializeField] float MaxVelocidadHorizontal;
    [SerializeField] float _forcejump;
    [SerializeField] float _checkjump;



    [SerializeField] AnimationCurve Aceleracion;
    float porCualPuntoDeAceleracionVa;

    // Update is called once per frame
    void Update()
    {
        NormalMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), false);
        
        Debug.DrawRay(transform.position, transform.up * -1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, transform.up * -1,_checkjump))
            {
                Jump();
            }
        }
    }

    public override void NormalMove(float horizontal, float vertical, bool enElAire)
    {
        if (horizontal != 0)
        {
            porCualPuntoDeAceleracionVa += Time.deltaTime;
        }

        porCualPuntoDeAceleracionVa = Mathf.Clamp(porCualPuntoDeAceleracionVa, 0, 1);

        rig.velocity = new Vector3(horizontal * Aceleracion.Evaluate(porCualPuntoDeAceleracionVa) * MaxVelocidadHorizontal, rig.velocity.y, 0);
    }

    void Jump()
    {
        rig.AddForce(transform.up * _forcejump);
    }
}
