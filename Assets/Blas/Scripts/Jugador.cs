using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : Personaje
{
    [Header ("Stats Movimiento")]
    [SerializeField] float MaxVelocidadHorizontal;

    [SerializeField] AnimationCurve Aceleracion;
    float porCualPuntoDeAceleracionVa;

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public override void Move(float horizontal, float vertical)
    {
        if(horizontal != 0)
        {
            porCualPuntoDeAceleracionVa += Time.deltaTime;
        }

        porCualPuntoDeAceleracionVa = Mathf.Clamp(porCualPuntoDeAceleracionVa, 0, 1);

        rig.velocity = new Vector3(horizontal * Aceleracion.Evaluate(porCualPuntoDeAceleracionVa) * MaxVelocidadHorizontal, rig.velocity.y, 0);
    }
}
