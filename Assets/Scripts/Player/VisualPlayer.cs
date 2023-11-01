using UnityEngine;

public class VisualPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public void PlayJumpParticles()
    {   
        _particleSystem.Play();
    }
}
