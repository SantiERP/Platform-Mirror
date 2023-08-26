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
    public override void Skill(Personaje personaje)
    {
       

       Debug.DrawRay(transform.position, transform.up * 3, Color.white);
       
       Debug.DrawRay(transform.position, -personaje.rig.velocity, Color.red);
       
       personaje.rig.velocity = Vector3.Reflect(personaje.rig.velocity, transform.up);
       
       
       Debug.DrawRay(transform.position, personaje.rig.velocity, Color.blue);
    }
}
