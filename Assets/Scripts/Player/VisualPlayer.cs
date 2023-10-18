using System.Collections;
using UnityEngine;

public class VisualPlayer : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    public void PlayJumpParticles()
    {   
        _particleSystem.transform.position = new Vector3(transform.position.x, transform.position.y-1);
        _particleSystem.Play();
    }
}
