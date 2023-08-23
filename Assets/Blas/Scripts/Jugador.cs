using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : Personaje
{
    [Header ("Stats Movimiento")]
    [SerializeField] float MaxVelocidadHorizontal;

    [SerializeField] AnimationCurve Aceleracion;
    float porCualPuntoDeAceleracionVa;

    float cuantoTiempoDesdeQueToqueElPiso = 0;
    [SerializeField] float tiempoCoyote;

    void FixedUpdate()
    {
        //Chequeo si estoy tocando el piso
        if(!TocandoElPiso())
        {
            //Si estoy en el aire, cuento el airtime
            cuantoTiempoDesdeQueToqueElPiso += Time.fixedDeltaTime;

            //Si estuve la cantidad de tiempo necesaria flotando, me empieza a afectar la gravedad
            if(cuantoTiempoDesdeQueToqueElPiso > tiempoCoyote)
            {
                rig.AddForce(Physics.gravity, ForceMode.Acceleration);
            }

            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), true);
        }

        //Si toque el piso, el contador vuelve a 0
        else
        {
            cuantoTiempoDesdeQueToqueElPiso = 0;

            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), false);
        }
    }

    bool TocandoElPiso()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1);
    }

    public override void Move(float horizontal, float vertical, bool enElAire)
    {
        #region Movimiento en el piso
        if(enElAire)
        {
            //Si te estas moviendo horizontalmente, aceleras en cierta direccion
            if(horizontal != 0)
            {
                porCualPuntoDeAceleracionVa += Time.deltaTime;
            }

            //Cambias la velocidad en base a cuanto estas moviendote
            porCualPuntoDeAceleracionVa = Mathf.Clamp(porCualPuntoDeAceleracionVa, 0, 1);

            rig.velocity = new Vector3(horizontal * Aceleracion.Evaluate(porCualPuntoDeAceleracionVa) * MaxVelocidadHorizontal, rig.velocity.y, 0);
        }
        #endregion

        #region Movimiento Aereo
        else
        {
            //Si te estas moviendo horizontalmente, aceleras en cierta direccion
            if(horizontal != 0)
            {
                porCualPuntoDeAceleracionVa += Time.deltaTime;
            }

            //Cambias la velocidad en base a cuanto estas moviendote
            porCualPuntoDeAceleracionVa = Mathf.Clamp(porCualPuntoDeAceleracionVa, 0, 1);

            rig.velocity = new Vector3(horizontal * Aceleracion.Evaluate(porCualPuntoDeAceleracionVa) * MaxVelocidadHorizontal, rig.velocity.y, 0);
        }
        #endregion
    }
}
