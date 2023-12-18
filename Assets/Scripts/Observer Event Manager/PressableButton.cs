using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[RequireComponent(typeof(AudioSource))]
public class PressableButton : Interruptor
{
    Vector3 _desiredPosition;
    Vector3 _normalPosition;

    int _amountOfObjectsPressing;

    AudioSource _audioSource;
    [SerializeField] GameObject[] _pointLight;

    void Start()
    {
        _normalPosition = transform.position;
        _desiredPosition = _normalPosition;

        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _desiredPosition, 0.01f);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing bounceable))
        {
            ButtonAction();
            _amountOfObjectsPressing++;
            Lights();
            if (_amountOfObjectsPressing == 1)
            {
                _audioSource.Play();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing bounceable))
        {
            _amountOfObjectsPressing--;
            Lights();
            if (_amountOfObjectsPressing <= 0)
            {
                ButtonAntiAction(); 
            }

        }
    }

    public override void NormalAction()
    {
        _desiredPosition = _normalPosition - transform.up * 0.8f;
    }

    public override void AntiAction()
    {
        _desiredPosition = _normalPosition;
    }

    void Lights()
    {
        foreach (GameObject pointLight in _pointLight)
        {
            pointLight.SetActive(!pointLight.activeSelf);
        }
    }
}
