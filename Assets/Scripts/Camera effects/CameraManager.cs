using UnityEngine;

public class CameraManager : MonoBehaviour, IMementeable
{
    enum infoPos
    {
        lastPos,
    }

    public static CameraManager Instance;
    [SerializeField] CameraPoints[] _camerasPoints;
    public int ActualPos = 0;
    bool lastMovementWasForward = true;

    public object[] Memories { get; set; }

    void Awake()
    {
        Instance = this;
        SaveManager.AddToSaveManager(this);
    }

    void Start()
    {
        _camerasPoints[0].SetCamera(0);
        Memories = new object[] {0};
    }

    #region Camera Movement
    public void NextPosition()
    {
        ActualPos++;
        _camerasPoints[ActualPos].SetCamera(1);
        lastMovementWasForward = true;
    }

    public void BeforePosition()
    {
        ActualPos--;
        _camerasPoints[ActualPos].SetCamera(-1);
        lastMovementWasForward = false;
    }
    #endregion

    #region Reload
    public void Load(int punto, Vector3 pos)
    {
        ActualPos = punto;
        transform.position = pos;
    }

    public void Remember()
    {
        if (Memories != null)
        {
            ActualPos = (int)Memories[(int)infoPos.lastPos];
            _camerasPoints[ActualPos].SetCamera(0);
        }
    }
    public void Forget()
    {
        Memories = null;
    }
    public void Save()
    {
        Memories = new object[] {ActualPos + 1};
    }
    #endregion
}
