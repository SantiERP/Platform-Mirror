using UnityEngine;

public class CameraManager : MonoBehaviour, IMementeable
{
    enum infoPos
    {
        lastPos,
    }

    public static CameraManager Instance;
    [SerializeField] CameraPoints[] _camerasPoints;
    public int actualPos = 0;

    public object[] _memories { get; set; }

    void Awake()
    {
        Instance = this;
        _camerasPoints[0].SetCamera(0);
        SaveManager.AddToSaveManager(this);
    }

    #region Camera Movement
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
    #endregion

    #region Reload
    public void Load(int punto, Vector3 pos)
    {
        actualPos = punto;
        transform.position = pos;
    }

    public void Remember()
    {
        if (_memories != null)
        {
            _camerasPoints[1 + (int)_memories[(int)infoPos.lastPos]].SetCamera(0);
            actualPos = 1 + (int)_memories[(int)infoPos.lastPos];
        }
    }
    public void Forget()
    {
        _memories = null;
    }
    public void Save()
    {
        _memories = new object[] { actualPos };
    }
    #endregion
}
