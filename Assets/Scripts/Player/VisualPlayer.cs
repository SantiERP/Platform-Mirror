using UnityEngine;

public class VisualPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    [Header ("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip Jump;
    [SerializeField] AudioClip Move;
    public void VisualJump()
    {
        _audioSource.clip = Jump;
        _audioSource.Play();
        _particleSystem.Play();
    }

    public void VisualMove()
    {
        if (_audioSource.clip != Move)
        {
            Debug.Log("Audio Move");
            _audioSource.clip = Move;
            _audioSource.Play();
            
        }
    }
}
