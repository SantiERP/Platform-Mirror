using UnityEngine;

public class Box : MonoBehaviour , IMementeable
{
    public object[] Memories { get; set; }
    Rigidbody _body;
    void Awake()
    {
        SaveManager.AddToSaveManager(this);
        _body = GetComponent<Rigidbody>();
    }
    public void Remember()
    {
        if (Memories != null)
        {
            _body.velocity = Vector3.zero;

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
