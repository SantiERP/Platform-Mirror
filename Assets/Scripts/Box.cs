using UnityEngine;

public class Box : MonoBehaviour , IMementeable
{
    public object[] Memories { get; set; }
    void Awake()
    {
        SaveManager.AddToSaveManager(this);
    }

    public void Remember()
    {
        if (Memories != null)
        { 
            transform.position = (Vector3)Memories[0];
            transform.rotation = (Quaternion)Memories[1];
        }
    }
    public void Forget()
    {
        Memories = null;
    }
    public void Save()
    {
        Memories = new object[] {transform.position , transform.rotation};
    }
}
