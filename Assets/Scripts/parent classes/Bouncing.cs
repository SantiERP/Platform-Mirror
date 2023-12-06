using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bouncing : MonoBehaviour
{
    public bool Small;
    Rigidbody _rb;
    Material _mat;
    [SerializeField] MeshRenderer _renderer;

    [SerializeField] Color _matColor = Color.white;

    public Rigidbody Rig
    {
        get
        {
            return _rb;
        }
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        if(_renderer == null)
        {
            _mat = GetComponent<MeshRenderer>().material;
        }
        else
        {
            _mat = _renderer.material;
        }

        _mat.color = _matColor;
        _mat.SetColor("Color" , _matColor);
    }

    public void Bounce(Vector3 mirrorPos, Vector3 mirrorNormal)
    {
        _mat.SetVector("_Pos" , mirrorPos);
        _mat.SetVector("_Normal" , mirrorNormal);
    }

    public void EndBounce()
    {
        _mat.SetVector("_Pos" , Vector3.up * -1000);
        _mat.SetVector("_Normal" , Vector3.up);
    }

}
