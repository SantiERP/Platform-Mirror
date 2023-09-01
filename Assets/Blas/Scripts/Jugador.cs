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

    [Header ("Stats Movimiento Aereo")]
    [SerializeField] float velocidadAerea;
    [SerializeField] AnimationCurve CurvaDeImportanciaDeApretarElBotonDeSalto;
    float tiempoApretandoElBotonDeSalto = 0;

    delegate void MovementType(float horizontal, float vertical, bool enElAire);

    MovementType Movement;

    void Awake()
    {
        EntityLister.JugadorT = transform;

        Movement += NormalMove;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_PlayerDead, Muerte);
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

            Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), true);
        }

        //Si toque el piso, el contador vuelve a 0
        else
        {
            cuantoTiempoDesdeQueToqueElPiso = 0;
            tiempoApretandoElBotonDeSalto = 0;

            Movement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), false);
        }
        
        if(Input.GetButton("Jump"))
        {
            tiempoApretandoElBotonDeSalto += Time.fixedDeltaTime;
            
            if(tiempoApretandoElBotonDeSalto < 0.5f)
            {
                Salto();
            }

            cuantoTiempoDesdeQueToqueElPiso = tiempoCoyote;
        }
    }

    bool TocandoElPiso()
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        return Physics.Raycast(transform.position + Vector3.right * 0.48f, -transform.up, 0.6F, layerMask) || Physics.Raycast(transform.position - Vector3.right * 0.48f, -transform.up, 0.6F, layerMask);
    }

    bool TocandoPared(int direccion)
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        direccion = Mathf.Clamp(direccion, -1, 1);

        return Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right * direccion, 0.5F, layerMask) || Physics.Raycast(transform.position - Vector3.up * 0.5f, Vector3.right * direccion, 0.5F, layerMask);        
    }

    public override void NormalMove(float horizontal, float vertical, bool enElAire)
    {
        #region Movimiento Aereo
        if(enElAire)
        {
            if(!TocandoPared((int)(horizontal)))
            {
                rig.AddForce(horizontal * Vector3.right * velocidadAerea);
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
        rig.AddForce(transform.up * fuerzaDeSalto * CurvaDeImportanciaDeApretarElBotonDeSalto.Evaluate(tiempoApretandoElBotonDeSalto) * Time.fixedDeltaTime, ForceMode.Impulse);
        // rig.velocity = Vector3.up * fuerzaDeSalto * CurvaDeImportanciaDeApretarElBotonDeSalto.Evaluate(tiempoApretandoElBotonDeSalto) + Vector3.right * rig.velocity.x;
    }

    void Muerte(object[] parameters)
    {

    }

    void FinalizarNivel()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_EndOfLevel, new object[1]);
    }
}
