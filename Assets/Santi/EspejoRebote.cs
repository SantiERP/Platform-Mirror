using UnityEngine;

public class EspejoRebote : Espejos
{
    public override void Skill(Rigidbody r)
    {
       Debug.DrawRay(transform.position, transform.up * 3, Color.white);
       
       Debug.DrawRay(transform.position, -r.velocity, Color.red);
       
       r.velocity = Vector3.Reflect(r.velocity, transform.up);

       r.AddForce(-Physics.gravity, ForceMode.Acceleration);

        Destroy(r);
       Debug.DrawRay(transform.position, r.velocity, Color.blue);
    }
}
