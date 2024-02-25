using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mirrors : MonoBehaviour 
{
    [SerializeField] AnimationCurve _characterEntrance;
    [SerializeField] ParticleSystem _throwParticles;
    [SerializeField] AudioSource _audioSource;

    List<Rigidbody> allRigs = new List<Rigidbody>();

    public static List<Mirrors> allMirrors = new List<Mirrors>();

    private void Awake()
    {
        allMirrors.Add(this);
    }

    public abstract void Skill(Bouncing b);
    public static float TimeSpeed = 0.01f;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing bounceable))
        {
            if (Vector3.Dot(transform.position - other.transform.position, transform.up) > 0) return;
            StartCoroutine(CharacterEntrance(bounceable.transform.position, bounceable.Rig, bounceable));
        }
    }
    
    IEnumerator CharacterEntrance(Vector3 intialPos, Rigidbody rig, Bouncing b)
    {
        AudioSource newAudio = null;
        if (_audioSource.isPlaying)
        {
            newAudio = gameObject.AddComponent<AudioSource>();
            newAudio.clip = _audioSource.clip;
            newAudio.Play();
        }
        else
        {
            _audioSource.Play();
        }

        Vector3 dir = rig.velocity;
        BoxCollider box = rig.GetComponent<BoxCollider>();  
        Bouncing charBouncing = rig.GetComponent<Bouncing>();
        charBouncing.Bounce(transform.position , transform.up);

        box.enabled = false;

        float actualInstant = 0f;
        Vector3 finalPos = intialPos + (Vector3.Reflect(dir + transform.up, transform.up).normalized);

        allRigs.Add(rig);
        rig.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        WaitForSeconds wait = new WaitForSeconds(0.01f);
        bool thrownPsrticles = false;

        while (actualInstant < 1)
        {
            rig.transform.position = Vector3.LerpUnclamped(intialPos, finalPos, _characterEntrance.Evaluate(actualInstant));
            actualInstant += TimeSpeed;

            if(actualInstant >= 0.4f && !thrownPsrticles)
            {
                ParticleSystem particle = Instantiate(_throwParticles);
                particle.transform.position = intialPos;
                particle.transform.forward = Vector3.Reflect(dir + transform.up, transform.up);
                particle.transform.position -= particle.transform.forward;
                thrownPsrticles = true;
            }

            yield return wait;
        }

        if(rig.GetComponent<ModelPlayer>())
        {
            rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        
        else
        {
            rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        }

        allRigs.Remove(rig);

        charBouncing.EndBounce();

        if(newAudio != null)
        {
            Destroy(newAudio);
        }

        box.enabled = true;
        Skill(b);
        Throw(dir, rig);
    }

    void Throw(Vector3 dir, Rigidbody r)
    {
        Vector3 newVelocity = Vector3.Reflect(dir - dir.normalized, transform.up);
        r.velocity = AtLeastNormalized(newVelocity, 5);

        Debug.Log($"<color=blue>{dir}</color>");
    }

    Vector3 AtLeastNormalized(Vector3 vector, float minimum)
    {
        if(vector.sqrMagnitude < minimum)
        {
            return vector.normalized * minimum;
        }

        return vector;
    }

    public void UnconstrainAllRigs()
    {
        foreach (Rigidbody rig in allRigs)
        {
            if (rig == null) continue;
            rig.GetComponent<Collider>().enabled = true;
            if (rig.GetComponent<ModelPlayer>())
            {
                rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }

            else
            {
                rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            }
        }
    }
}
