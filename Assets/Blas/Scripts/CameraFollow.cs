using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform target;

    Vector3 toVector2;

    [SerializeField][Range (0, 0.2f)] float VelocidadDeSeguimiento;

    // Start is called before the first frame update
    void Start()
    {
        toVector2 = Vector3.right + Vector3.up;

        target = EntityLister.Jugador;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, CamPos(target), VelocidadDeSeguimiento);
    }

    Vector3 CamPos(Transform pos)
    {
        return new Vector3(pos.position.x, pos.position.y, transform.position.z);
    }
}
