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

    [SerializeField] int _IntensityExit;
    [SerializeField] int _IntensityEnter;

    void Start()
    {
        _normalPosition = transform.position;
        _desiredPosition = _normalPosition;

        _audioSource = GetComponent<AudioSource>();
        Lights(_IntensityExit);
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
            Lights(_IntensityEnter);
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
            Lights(_IntensityExit);
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

    void Lights(int i)
    {
        foreach (GameObject pointLight in _pointLight)
        {
            pointLight.SetActive(true);
            pointLight.GetComponent<Light>().intensity = i;
        }
    }
}
