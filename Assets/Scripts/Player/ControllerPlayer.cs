using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(ModelPlayer))]
public class ControllerPlayer : MonoBehaviour
{
    VisualPlayer _visual;
    bool _activateparticle;

    ModelPlayer jugador;
    void Awake()
    {
        jugador = GetComponent<ModelPlayer>();
        _visual = GetComponentInChildren<VisualPlayer>();

    }

    void FixedUpdate()
    {
        /*
        //Chequeo si estoy tocando el piso
        if (!jugador.TocandoElPiso())
        {
            
            //Si estoy en el aire, cuento el airtime
            cuantoTiempoDesdeQueToqueElPiso += Time.fixedDeltaTime;

            //Si estuve la cantidad de tiempo necesaria flotando, me empieza a afectar la gravedad
            if (cuantoTiempoDesdeQueToqueElPiso > tiempoCoyote)
            {
                rig.AddForce(Physics.gravity, ForceMode.Acceleration);
            }

            jugador.NormalMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), true);
            
    }

        //Si toque el piso, el contador vuelve a 0
        else
        {
            cuantoTiempoDesdeQueToqueElPiso = 0;
            tiempoApretandoElBotonDeSalto = 0;
            jugador.NormalMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), false);
        }
        *///Vieja Manera
        jugador.NormalMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), true);

        if (Input.GetButton("Jump"))
        {
            /*
            jugador.Salto(fuerzaDeSalto, CurvaDeImportanciaDeApretarElBotonDeSalto, tiempoApretandoElBotonDeSalto);

            tiempoApretandoElBotonDeSalto += Time.fixedDeltaTime;

            if (tiempoApretandoElBotonDeSalto < 0.5f)
            {
                jugador.Salto( fuerzaDeSalto,  CurvaDeImportanciaDeApretarElBotonDeSalto,  tiempoApretandoElBotonDeSalto);
            }

            cuantoTiempoDesdeQueToqueElPiso = tiempoCoyote;
            */
            jugador.Salto();
        }
        if (Input.GetButtonDown("Jump"))
        {
           _visual.PlayParticles();
        }

    }

    private void Update()
    {
        if(Input.GetButtonDown("Restart"))
        {
            SceneManagement.ReloadScene();
        }
    }
}
