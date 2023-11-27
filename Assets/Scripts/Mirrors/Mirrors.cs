using System.Collections;
using UnityEngine;

public abstract class Mirrors : MonoBehaviour 
{
    [SerializeField] AnimationCurve characterEntrance;
    [SerializeField] ParticleSystem throwParticles;

    public abstract void Skill( Rigidbody rig);

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bouncing>(out Bouncing rebotable))
        {
            StartCoroutine(CharacterEntrance(rebotable.transform.position, rebotable.rig));
        }
    }

    IEnumerator CharacterEntrance(Vector3 intialPos, Rigidbody rig)
    {
        Vector3 dir = rig.velocity;
        BoxCollider box = rig.GetComponent<BoxCollider>();  
        Bouncing charBouncing = rig.GetComponent<Bouncing>();
        charBouncing.Bounce(transform.position , transform.up);

        box.enabled = false;


        float actualInstant = 0f;
        Vector3 finalPos = intialPos + (Vector3.Reflect(dir + transform.up, transform.up).normalized);

        rig.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        WaitForSeconds wait = new WaitForSeconds(0.01f);
        bool thrownPsrticles = false;

        while (actualInstant < 1)
        {
            rig.transform.position = Vector3.LerpUnclamped(intialPos, finalPos, characterEntrance.Evaluate(actualInstant));

            actualInstant += 0.01f;

            if(actualInstant >= 0.4f && !thrownPsrticles)
            {
                ParticleSystem particle = Instantiate(throwParticles);
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
            rig.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }

        charBouncing.EndBounce();

        box.enabled = true;        
        Skill(rig);
        Throw(dir, rig);
    }

    void Throw(Vector3 dir, Rigidbody r)
    {
        r.velocity = Vector3.Reflect(dir - dir.normalized, transform.up);
        //Normalize the vector

        Debug.Log($"<color=blue>{dir}</color>");
    }
}
