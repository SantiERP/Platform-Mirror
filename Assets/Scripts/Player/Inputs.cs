using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Jugador))]
public class Inputs : MonoBehaviour
{
    Jugador jugador;

    Rigidbody rig;

    [Header("Stats Movimiento")]
    [SerializeField] float MaxVelocidadHorizontal;
    [SerializeField] AnimationCurve Aceleracion;
    [SerializeField] float tiempoCoyote;
    [SerializeField] float cuantoTiempoDesdeQueToqueElPiso = 0;

    [SerializeField] float fuerzaDeSalto;

    [Header("Stats Movimiento Aereo")]
    [SerializeField] float velocidadAerea;
    [SerializeField] AnimationCurve CurvaDeImportanciaDeApretarElBotonDeSalto;
    float tiempoApretandoElBotonDeSalto = 0;


    void Awake()
    {
        jugador = GetComponent<Jugador>();

        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
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

            jugador.NormalMove(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), true,  velocidadAerea, MaxVelocidadHorizontal,  Aceleracion);
        }

        //Si toque el piso, el contador vuelve a 0
        else
        {
            cuantoTiempoDesdeQueToqueElPiso = 0;
            tiempoApretandoElBotonDeSalto = 0;
            jugador.NormalMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), false, velocidadAerea, MaxVelocidadHorizontal, Aceleracion);
        }

        if (Input.GetButton("Jump"))
        {
            tiempoApretandoElBotonDeSalto += Time.fixedDeltaTime;

            if (tiempoApretandoElBotonDeSalto < 0.5f)
            {
                jugador.Salto( fuerzaDeSalto,  CurvaDeImportanciaDeApretarElBotonDeSalto,  tiempoApretandoElBotonDeSalto);
            }

            cuantoTiempoDesdeQueToqueElPiso = tiempoCoyote;
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
