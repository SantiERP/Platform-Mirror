using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : Personaje
{
    [Header ("Stats Movimiento")]
    [SerializeField] float MaxVelocidadHorizontal;

    [SerializeField] AnimationCurve Aceleracion;
    float porCualPuntoDeAceleracionVa;
    [SerializeField]float tiempoCoyote;
    float cuantoTiempoDesdeQueToqueElPiso = 0;

    [SerializeField] float fuerzaDeSalto;

    void Awake()
    {
        EntityLister.Jugador = transform;
    }

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

            
            if(Input.GetButton("Jump"))
            {
                Salto();
                cuantoTiempoDesdeQueToqueElPiso = tiempoCoyote;
            }
        }
    }

    bool TocandoElPiso()
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        return Physics.Raycast(transform.position, Vector3.down, 0.6F, layerMask);
    }

    bool TocandoPared(int direccion)
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        direccion = Mathf.Clamp(direccion, -1, 1);
        Debug.Log(Physics.Raycast(transform.position, Vector3.right * direccion, 0.5F));

        return Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right * direccion, 0.5F, layerMask) || Physics.Raycast(transform.position - Vector3.up * 0.5f, Vector3.right * direccion, 0.5F, layerMask);        
    }

    public override void Move(float horizontal, float vertical, bool enElAire)
    {
        #region Movimiento Aereo
        if(enElAire)
        {
            if(!TocandoPared((int)(horizontal)))
            {
                rig.AddForce(horizontal * Vector3.right * 5);
            }
        }
        #endregion

        #region Movimiento Terrestre
        else
        {
            //Si te estas moviendo horizontalmente, aceleras en cierta direccion
            if(horizontal != 0)
            {
                porCualPuntoDeAceleracionVa += Time.fixedDeltaTime;
            }

            //Cambias la velocidad en base a cuanto estas moviendote
            porCualPuntoDeAceleracionVa = Mathf.Clamp(porCualPuntoDeAceleracionVa, 0, 1);

            //rig.velocity = new Vector3(horizontal * Aceleracion.Evaluate(porCualPuntoDeAceleracionVa) * MaxVelocidadHorizontal, rig.velocity.y, 0);

            if(!TocandoPared((int)(horizontal)))
            {
                rig.velocity = new Vector3(horizontal * Aceleracion.Evaluate(porCualPuntoDeAceleracionVa) * MaxVelocidadHorizontal, rig.velocity.y, 0);
            }
        }
        #endregion
    }

    void Salto()
    {
        rig.velocity = Vector3.up * fuerzaDeSalto + Vector3.right * rig.velocity.x;
    }
}
