using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Interruptor : MonoBehaviour
{
    public delegate void Action();
    public Action ButtonAction;
    public Action ButtonAntiAction;

    void Awake()
    {
        ButtonAction += NormalAction;
        ButtonAntiAction += AntiAction;
    }

    // Update is called once per frame

    public abstract void NormalAction();

    public abstract void AntiAction();
}
