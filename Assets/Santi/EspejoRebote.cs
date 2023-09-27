using UnityEngine;

public class EspejoRebote : Espejos
{
    public override void Skill(Rigidbody r)
    {
        //Debug.DrawRay(transform.position, transform.up * 3, Color.white);
       //Debug.Log(transform.up);
       //Debug.DrawRay(r.transform.position, Vector3.Reflect(r.velocity + Physics.gravity * Time.fixedDeltaTime, transform.up), Color.red);
       //Debug.Log(Vector3.Reflect(r.velocity + Physics.gravity * Time.fixedDeltaTime, transform.up));
        
       //r.velocity = Vector3.Reflect(r.velocity + Physics.gravity*Time.fixedDeltaTime, transform.up);
        
      // r.AddForce(-Physics.gravity, ForceMode.Acceleration);

       //Debug.DrawRay(transform.position, r.velocity, Color.blue);
    }
}
