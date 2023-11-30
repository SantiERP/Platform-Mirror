using UnityEngine;

public abstract class InstantiateBullet : MonoBehaviour
{
    Bullet _bullet;

    public virtual void Shot(Vector3 v)
    {
        _bullet = BulletFactory.Instance.GetObjectFromPool();
        _bullet.transform.position = v;
    }
}
