using System.Collections;
using UnityEngine;

public abstract class Mirrors : MonoBehaviour 
{
    [SerializeField] AnimationCurve characterEntrance;

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

        box.enabled = false;


        float actualInstant = 0f;
        Vector3 finalPos = intialPos + (Vector3.Reflect(dir + transform.up, transform.up).normalized);

        rig.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        WaitForSeconds wait = new WaitForSeconds(0.01f);

        while (actualInstant < 1)
        {
            rig.transform.position = Vector3.LerpUnclamped(intialPos, finalPos, characterEntrance.Evaluate(actualInstant));

            actualInstant += 0.01f;

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
