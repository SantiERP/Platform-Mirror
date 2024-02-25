using UnityEngine;

public class VisualPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    [Header ("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _jump;
    [SerializeField] AudioClip _move;

    public delegate void Action();

    public Action Jump;
    public static VisualPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Jump = VisualJump;
    }
    public void VisualJump()
    {
        if(_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = _jump;
        _audioSource.Play();
        _particleSystem.Play();
    }
    public void TinyJump()
    {

    } 
    public void VisualMove()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = _move;
            _audioSource.Play();
            
        }
    }

    public void VisualStopMove()
    {
        if(_audioSource.clip == _move && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
