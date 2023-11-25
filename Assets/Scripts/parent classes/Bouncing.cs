using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bouncing : MonoBehaviour
{
    Rigidbody rb;
    Material mat;
    [SerializeField] MeshRenderer renderer;

    [SerializeField] Color MatColor = Color.white;

    public Rigidbody rig
    {
        get
        {
            return rb;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(renderer == null)
        {
            mat = GetComponent<MeshRenderer>().material;
        }
        else
        {
            mat = renderer.material;
        }

        mat.color = MatColor;
        mat.SetColor("Color" , MatColor);
    }

    public void Bounce(Vector3 mirrorPos, Vector3 mirrorNormal)
    {
        mat.SetVector("_Pos" , mirrorPos);
        mat.SetVector("_Normal" , mirrorNormal);
    }

    public void EndBounce()
    {
        mat.SetVector("_Pos" , Vector3.up * -1000);
        mat.SetVector("_Normal" , Vector3.up);
    }

}
