using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jugador : Personaje
{

    void Awake()
    {
        EntityLister.JugadorT = transform;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_PlayerDead, Muerte);
    }


    public bool TocandoElPiso()
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        return Physics.Raycast(transform.position + Vector3.right * 0.48f, -transform.up, 0.6F, layerMask) || Physics.Raycast(transform.position - Vector3.right * 0.48f, -transform.up, 0.6F, layerMask);
    }

    public bool TocandoPared(int direccion)
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        direccion = Mathf.Clamp(direccion, -1, 1);
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, Vector3.right * direccion, Color.yellow);
        return Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right * direccion, 0.5F, layerMask) || Physics.Raycast(transform.position - Vector3.up * 0.5f, Vector3.right * direccion, 0.5F, layerMask);        
    }

    public override void NormalMove(float horizontal, float vertical, bool enElAire, float velocidadAerea, float MaxVelocidadHorizontal, AnimationCurve Aceleracion)
    {
        float porCualPuntoDeAceleracionVa = 0;
        #region Movimiento Aereo
        if (enElAire)
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
            Debug.Log("estoy en el piso");

            //Si te estas moviendo horizontalmente, aceleras en cierta direccion
            if (horizontal != 0)
            {
                porCualPuntoDeAceleracionVa += Time.fixedDeltaTime;
            }

            //Cambias la velocidad en base a cuanto estas moviendote
            porCualPuntoDeAceleracionVa = Mathf.Clamp(porCualPuntoDeAceleracionVa, 0, 1);


            if(!TocandoPared((int)(horizontal)))
            {
                rig.velocity = new Vector3(horizontal * Aceleracion.Evaluate(porCualPuntoDeAceleracionVa) * MaxVelocidadHorizontal, rig.velocity.y, 0);
            }
        }
        #endregion
    }

    public void Salto(float fuerzaDeSalto, AnimationCurve CurvaDeImportanciaDeApretarElBotonDeSalto,float tiempoApretandoElBotonDeSalto)
    {
        rig.AddForce(transform.up * fuerzaDeSalto * CurvaDeImportanciaDeApretarElBotonDeSalto.Evaluate(tiempoApretandoElBotonDeSalto) * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    void Muerte(object[] parameters)
    {

    }

    void FinalizarNivel()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_EndOfLevel, new object[1]);
    }
}
