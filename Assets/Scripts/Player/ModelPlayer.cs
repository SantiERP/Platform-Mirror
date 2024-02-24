using System.Security.Cryptography;
using UnityEngine;

public class ModelPlayer : Player, IMementeable
{
    public static ModelPlayer Entity;

    [SerializeField] float _onWhichAccelerationPointItsIn = 0;

    [Header("Stats Movimiento")]
    [SerializeField] float _maxHorizontalSpeed;
    [SerializeField] AnimationCurve _acceleration;
    [SerializeField] float _coyoteTime = 0.1f;
    [SerializeField] float _howLongSinceITouchedTheFloor = 0;

    float _jumpStrength;
    [SerializeField] float _normalJumpStrength = 40;

    public float JumpStrenght { get => _jumpStrength; set { Debug.Log(value); _jumpStrength = value; } }
    public float NormalJump { get => _normalJumpStrength; }
    [SerializeField] float _gravityMultiplier;
    public float GravityMultiplier { get => _gravityMultiplier; set { _gravityMultiplier = value; } }
    [Header("Stats Movimiento Aereo")]
    [SerializeField] float _airVelocity = 6;
    [SerializeField] AnimationCurve _importanceCurveOfPressingJumpButton;
    float _timePressingJumpButton = 0;
    [SerializeField] bool stopedJumping = true;

    IController _controller;
    VisualPlayer _visual;

    void Awake()
    {
        _visual = GetComponentInChildren<VisualPlayer>();
        _controller = new ControllerPlayer(_visual, this);

        Entity = this;

        EntityLister.PlayerT = this.transform;

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_PlayerDead, delegate { SaveManager.Remember();});

        EventManager.SubscribeToEvent(EventManager.EventsType.Event_Restart, LoadLastPos);

        SaveManager.AddToSaveManager(this);
        _jumpStrength = _normalJumpStrength;
        Debug.Log(_controller);
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
        float scale = 1;
        if(Small)
            scale = 0.5f;

        int layerMask = 4 << 6;
        layerMask = ~layerMask;
        RaycastHit hit;
        Physics.Raycast(transform.position + transform.right * 0.48f * scale, -transform.up, out hit, 0.6F * transform.localScale.y, layerMask, QueryTriggerInteraction.Ignore);
        rig = hit.rigidbody;

        bool answer = Physics.CheckBox(transform.position - transform.up, Vector3.one * 0.5f * scale, Quaternion.identity, layerMask , QueryTriggerInteraction.Ignore);

        if (answer)
        {
            stopedJumping = false;
        }

        return answer;
    }

    public bool TouchingTheFloor()
    {
        float scale = 1;
        if (Small)
            scale = 0.5f;

        int layerMask = 4 << 6;
        layerMask = ~layerMask;
        bool answer = Physics.CheckBox(transform.position - transform.up * .6f * scale, (transform.up * 0.05f + Vector3.forward + Vector3.right * .49f) * scale, Quaternion.identity, layerMask,QueryTriggerInteraction.Ignore);

        if (answer)
        {
            stopedJumping = false;
        }

        return answer;
    }

    public bool TouchingTheWall(int direccion)
    {
        int layerMask = 1 << 6;
        layerMask = ~layerMask;

        direccion = Mathf.Clamp(direccion, -1, 1);
        Debug.DrawRay(transform.position + transform.up * 0.5f, transform.right * direccion, Color.yellow);
        return Physics.Raycast(transform.position + transform.up * 0.5f, transform.right * direccion, 0.5F, layerMask , QueryTriggerInteraction.Ignore) || Physics.Raycast(transform.position - transform.up * 0.5f, transform.right * direccion, 0.5F, layerMask, QueryTriggerInteraction.Ignore);        
    }

    public override void NormalMove(float horizontal, float vertical)
    {
        //if I was floating for long enought, gravity starts to afect me
        if (_howLongSinceITouchedTheFloor > _coyoteTime)
        {
            Rig.AddForce(Physics.gravity * _gravityMultiplier, ForceMode.Acceleration);
        }

        #region Terrestrial Movement
        if (TouchingTheFloor())
        {
            if (!Input.GetButton("Jump"))
            {
                Rig.velocity = new Vector3(Rig.velocity.x, 0, 0);
                _howLongSinceITouchedTheFloor = 0;
                _timePressingJumpButton = 0;
            }

            if (horizontal != 0)
            {
                _visual.VisualMove();
                _onWhichAccelerationPointItsIn += Time.fixedDeltaTime;
            }
            else
            {
                _visual.VisualStopMove();
                _onWhichAccelerationPointItsIn -= Time.fixedDeltaTime;
            }

            _onWhichAccelerationPointItsIn = Mathf.Clamp(_onWhichAccelerationPointItsIn, 0, 1);

            if (!TouchingTheWall((int)(horizontal)))
            {
                Rig.velocity = new Vector3(horizontal * _acceleration.Evaluate(_onWhichAccelerationPointItsIn) * _maxHorizontalSpeed, Rig.velocity.y, 0);
            }
        }
        #endregion

        #region Aerial Movement
        else
        {
            if(!Input.GetButton("Jump"))
            {
                _timePressingJumpButton = 1;
            }

            _howLongSinceITouchedTheFloor += Time.fixedDeltaTime;

            if (!TouchingTheWall((int)(horizontal)))
            {
                Rig.AddForce(horizontal * transform.right * _airVelocity);
            }
        }
        #endregion
    }

    public void Jump()
    {
        _timePressingJumpButton += Time.fixedDeltaTime;

        if (_timePressingJumpButton < 0.5f && !stopedJumping)
        {
            Rig.AddForce(transform.up * _jumpStrength * _importanceCurveOfPressingJumpButton.Evaluate(_timePressingJumpButton) * Time.fixedDeltaTime, ForceMode.Impulse);
            Rigidbody boxRigidbody;
            TouchingTheFloor(out boxRigidbody);

            if(boxRigidbody != null)
            { 
                boxRigidbody.AddForce(-transform.up * _jumpStrength * _importanceCurveOfPressingJumpButton.Evaluate(_timePressingJumpButton) * Time.fixedDeltaTime, ForceMode.Impulse);
            }
        }

    }
    public void StopJumping()
    {
        stopedJumping = true;
        _timePressingJumpButton = 1;
    }
    
    
    #region Remembering
    [SerializeField] public object[] Memories { get; set; }

    public void Remember()
    {
        if (Memories != null)
        {
            transform.position = (Vector3)Memories[0];
            transform.rotation = (Quaternion)Memories[1];
            Physics.gravity = (Vector3)Memories[2];

            //Si esta chibieado
            if((bool)Memories[3])
            {
                transform.localScale = Vector3.one * 0.5f;
                JumpStrenght = 0;
                GravityMultiplier = 0.5f;
                _visual.Jump = delegate { _visual.TinyJump(); };
            }
            else
            {
                transform.localScale = Vector3.one;
                JumpStrenght = NormalJump;
                _visual.Jump = delegate { _visual.VisualJump(); };
                GravityMultiplier = 1;
            }

            Small = (bool)Memories[3];
        }

        EndBounce();
    }
    public void Forget()
    {
        Memories = null;
    }
    public void Save()
    {
        Memories = new object[] { transform.position, transform.rotation , Physics.gravity , (transform.localScale.x == 0.5f) };
    }
    #endregion
}
