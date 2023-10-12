using System.Security.Cryptography;
using UnityEngine;

public class ModelPlayer : Player
{
    public static ModelPlayer entity;

    [SerializeField] float porCualPuntoDeAceleracionVa = 0;

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

    IController _controller;
    VisualPlayer _visual;


    void Awake()
    {
        _controller = new ControllerPlayer(GetComponentInChildren<VisualPlayer>(), this);

        Debug.Log("Awake");

        entity = this;

        EntityLister.JugadorT = transform;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_PlayerDead, delegate { SceneManagement.ReloadScene();});

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Restart, LoadLastPos);

        SaveManager.Load();

    }

    void LoadLastPos(object[] parameters)
    {
        this.transform.position = SaveManager.Data.lastPos;
    }

    private void Update()
    {
        _controller.UpdateInput();
    }

    private void FixedUpdate()
    {
        _controller.FixedUpdateInput();
    }
 
    
    public bool TocandoElPiso()
    {
        int layerMask = 4 << 6;
        layerMask = ~layerMask;

        return Physics.Raycast(transform.position + Vector3.right * 0.48f, -transform.up, 0.6F, layerMask) || Physics.Raycast(transform.position - Vector3.right * 0.48f, -transform.up, 0.6F, layerMask, QueryTriggerInteraction.Ignore);
    }

    public bool TocandoPared(int direccion)
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        direccion = Mathf.Clamp(direccion, -1, 1);
        Debug.DrawRay(transform.position + Vector3.up * 0.5f, Vector3.right * direccion, Color.yellow);
        return Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.right * direccion, 0.5F, layerMask) || Physics.Raycast(transform.position - Vector3.up * 0.5f, Vector3.right * direccion, 0.5F, layerMask, QueryTriggerInteraction.Ignore);        
    }

    public override void NormalMove(float horizontal, float vertical)
    {
        #region Movimiento Aereo
        if (!TocandoElPiso())
        {
            cuantoTiempoDesdeQueToqueElPiso += Time.fixedDeltaTime;

            //Si estuve la cantidad de tiempo necesaria flotando, me empieza a afectar la gravedad
            if (cuantoTiempoDesdeQueToqueElPiso > tiempoCoyote)
            {
                rig.AddForce(Physics.gravity, ForceMode.Acceleration);
            }
            if (!TocandoPared((int)(horizontal)))
            {
                rig.AddForce(horizontal * transform.right * velocidadAerea);
            }
        }
        #endregion

        #region Movimiento Terrestre
        else
        {
            cuantoTiempoDesdeQueToqueElPiso = 0;
            tiempoApretandoElBotonDeSalto = 0;

            //Si te estas moviendo horizontalmente, aceleras en cierta direccion
            if (horizontal != 0)
            {
                porCualPuntoDeAceleracionVa += Time.fixedDeltaTime;
            }
            else
            {
                porCualPuntoDeAceleracionVa -= Time.fixedDeltaTime;
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

    public void Salto()
    {
        tiempoApretandoElBotonDeSalto += Time.fixedDeltaTime;

        if (tiempoApretandoElBotonDeSalto < 0.5f)
        {
            rig.AddForce(transform.up * fuerzaDeSalto * CurvaDeImportanciaDeApretarElBotonDeSalto.Evaluate(tiempoApretandoElBotonDeSalto) * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        cuantoTiempoDesdeQueToqueElPiso = tiempoCoyote;
    }

    void FinalizarNivel()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_EndOfLevel, new object[1]);
    }
}
