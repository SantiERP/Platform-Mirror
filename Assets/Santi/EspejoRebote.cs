using UnityEngine;

public class EspejoRebote : Espejos
{
    /*
    private void OnTriggerEnter(Collider other)
    {
        Personaje personaje = other.GetComponent<Personaje>();
        if (personaje != null)
        {
            Skill(personaje.GetComponent<Rigidbody>());
        }

    }
    */
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Personaje personaje = collision.gameObject.GetComponent<Personaje>();
        if (personaje != null)
        {
            Skill(personaje.GetComponent<Rigidbody>());
        }
    }
    */
    public override void Skill(Rigidbody rig)
    {
       rig.velocity = Vector3.Reflect(rig.velocity, transform.up);
    }
}
