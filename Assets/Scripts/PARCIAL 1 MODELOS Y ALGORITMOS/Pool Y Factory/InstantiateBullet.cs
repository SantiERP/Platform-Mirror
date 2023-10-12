using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InstantiateBullet : MonoBehaviour
{
    Bullet b;

    public virtual void Shot(Vector3 v)
    {
        b = BulletFactory.Instance.GetObjectFromPool();
        b.transform.position = v;
    }
}
