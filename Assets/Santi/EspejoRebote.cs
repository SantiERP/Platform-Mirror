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
    public override void Skill(Rigidbody r)
    {
       Debug.DrawRay(transform.position, transform.up * 3, Color.white);
       
       Debug.DrawRay(transform.position, -r.velocity, Color.red);
       
       r.velocity = Vector3.Reflect(r.velocity, transform.up);

       r.AddForce(-Physics.gravity, ForceMode.Acceleration);
       
       Debug.DrawRay(transform.position, r.velocity, Color.blue);
    }
}
