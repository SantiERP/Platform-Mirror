using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] CameraPoints[] _camerasPoints;
    public int actualPos = 0;

    void Awake()
    {
        Instance = this;
        _camerasPoints[0].SetCamera(0);
    }

    public void NextPosition()
    {
        actualPos++;
        _camerasPoints[actualPos].SetCamera(1);
    }

    public void BeforePosition()
    {
        actualPos--;
        _camerasPoints[actualPos].SetCamera(-1);
    }

    public void Load(int punto, Vector3 pos)
    {
        actualPos = punto;
        transform.position = pos;
    }
}
