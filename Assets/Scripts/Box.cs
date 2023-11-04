using UnityEngine;

public class Box : MonoBehaviour , IMementeable
{
    public object[] _memories { get; set; }
    void Awake()
    {
        SaveManager.AddToSaveManager(this);
    }

    public void Remember()
    {
        if (_memories != null)
        { 
            transform.position = (Vector3)_memories[0];
            transform.rotation = (Quaternion)_memories[1];
        }
    }
    public void Forget()
    {
        _memories = null;
    }
    public void Save()
    {
        _memories = new object[] {transform.position , transform.rotation};
    }
}
