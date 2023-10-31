using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] PuntosCamaras[] _camerasPoints;
    public int actualPos = 0;

    void Awake()
    {
        Instance = this;
        _camerasPoints[0].SetCamera();
    }

    public void NextPosition()
    {
        actualPos++;
        _camerasPoints[actualPos].SetCamera();
    }

    public void BeforePosition()
    {
        actualPos--;
        _camerasPoints[actualPos].SetCamera();
    }

    public void Load(int punto, Vector3 pos)
    {
        actualPos = punto;
        transform.position = pos;
    }
}
