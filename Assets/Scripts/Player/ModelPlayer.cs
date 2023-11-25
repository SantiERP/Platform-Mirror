using System.Security.Cryptography;
using UnityEngine;

public class ModelPlayer : Player , IMementeable
{
    public static ModelPlayer entity;

    [SerializeField] float _onWhichAccelerationPointItsIn = 0;

    [Header("Stats Movimiento")]
    [SerializeField] float _maxHorizontalSpeed;
    [SerializeField] AnimationCurve _acceleration;
    [SerializeField] float _coyoteTime = 0.1f;
    [SerializeField] float _howLongSinceITouchedTheFloor = 0;

    [SerializeField] float _jumpStrength = 35;

    [Header("Stats Movimiento Aereo")]
    [SerializeField] float _airVelocity = 6;
    [SerializeField] AnimationCurve _importanceCurveOfPressingJumpButton;
    float _timePressingJumpButton = 0;

    IController _controller;
    VisualPlayer _visual;

    void Awake()
    {
        _visual = GetComponentInChildren<VisualPlayer>();
        _controller = new ControllerPlayer(_visual, this);

        entity = this;

        EntityLister.PlayerT = this.transform;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_PlayerDead, delegate { SaveManager.Remember();});

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Restart, LoadLastPos);
        
        SaveManager.AddToSaveManager(this);
        SaveManager.Save();
    }

    void LoadLastPos(object[] parameters)
    {

    }

    private void Update()
    {
        _controller.UpdateInput();
    }

    private void FixedUpdate()
    {
        _controller.FixedUpdateInput();
    }
 
    
    public bool TouchingTheFloor(out Rigidbody rig)
    {
        int layerMask = 4 << 6;
        layerMask = ~layerMask;
        RaycastHit hit;
        Physics.Raycast(transform.position + transform.right * 0.48f, -transform.up, out hit, 0.6F, layerMask);
        rig = hit.rigidbody;
        return Physics.Raycast(transform.position + transform.right * 0.48f, -transform.up, 0.6F, layerMask) || Physics.Raycast(transform.position - transform.right * 0.48f, -transform.up, 0.6F, layerMask, QueryTriggerInteraction.Ignore);
    }

    public bool TouchingTheFloor()
    {
        int layerMask = 4 << 6;
        layerMask = ~layerMask;
        return Physics.Raycast(transform.position + transform.right * 0.48f, -transform.up, 0.6F, layerMask) || Physics.Raycast(transform.position - transform.right * 0.48f, -transform.up, 0.6F, layerMask, QueryTriggerInteraction.Ignore);
    }

    public bool TouchingTheWall(int direccion)
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        direccion = Mathf.Clamp(direccion, -1, 1);
        Debug.DrawRay(transform.position + transform.up * 0.5f, transform.right * direccion, Color.yellow);
        return Physics.Raycast(transform.position + transform.up * 0.5f, transform.right * direccion, 0.5F, layerMask) || Physics.Raycast(transform.position - transform.up * 0.5f, transform.right * direccion, 0.5F, layerMask, QueryTriggerInteraction.Ignore);        
    }

    public override void NormalMove(float horizontal, float vertical)
    {
        #region Aerial Movement
        if (!TouchingTheFloor())
        {
            _howLongSinceITouchedTheFloor += Time.fixedDeltaTime;

            //if I was floating for long enought, gravity starts to afect me
            if (_howLongSinceITouchedTheFloor > _coyoteTime)
            {
                rig.AddForce(Physics.gravity, ForceMode.Acceleration);
            }
            if (!TouchingTheWall((int)(horizontal)))
            {
                rig.AddForce(horizontal * transform.right * _airVelocity);
            }
        }
        #endregion

        #region Terrestial Movement

        else
        {
            if (rig.velocity.y <= 0)
            { 
                _howLongSinceITouchedTheFloor = 0; 
                _timePressingJumpButton = 0;
            }

            //Si te estas moviendo horizontalmente, aceleras en cierta direccion
            if (horizontal != 0)
            {
                _visual.VisualMove();
                _onWhichAccelerationPointItsIn += Time.fixedDeltaTime;
            }
            else
            {
                _onWhichAccelerationPointItsIn -= Time.fixedDeltaTime;
            }

            //You Change the Velocity based on how much you are moving
            _onWhichAccelerationPointItsIn = Mathf.Clamp(_onWhichAccelerationPointItsIn, 0, 1);

            if(!TouchingTheWall((int)(horizontal)))
            {
                rig.velocity = new Vector3(horizontal * _acceleration.Evaluate(_onWhichAccelerationPointItsIn) * _maxHorizontalSpeed, rig.velocity.y, 0);
            } 
        }
        #endregion
    }

    public void Jump()
    {
        _timePressingJumpButton += Time.fixedDeltaTime;

        if (_timePressingJumpButton < 0.5f)
        {
            rig.AddForce(transform.up * _jumpStrength * _importanceCurveOfPressingJumpButton.Evaluate(_timePressingJumpButton) * Time.fixedDeltaTime, ForceMode.Impulse);
            Rigidbody boxRigidbody;
            TouchingTheFloor(out boxRigidbody);
            try
            {
                boxRigidbody.AddForce(-transform.up * _jumpStrength * _importanceCurveOfPressingJumpButton.Evaluate(_timePressingJumpButton) * Time.fixedDeltaTime, ForceMode.Impulse);
            }
            catch
            {

            }
        }

        //_howLongSinceITouchedTheFloor = _coyoteTime;
    }

    void FinalizeLevel()
    {
        EventManager.TriggerEvent(EventManager.EventsType.Event_EndOfLevel, new object[1]);
    }

    #region Remembering
    [SerializeField] public object[] _memories { get; set; }

    public void Remember()
    {
        if (_memories != null)
        {
            transform.position = (Vector3)_memories[0];
            transform.rotation = (Quaternion)_memories[1];
            Physics.gravity = (Vector3)_memories[2];
        }
    }
    public void Forget()
    {
        _memories = null;
    }
    public void Save()
    {
        _memories = new object[] { transform.position, transform.rotation , Physics.gravity};
    }
    #endregion
}
