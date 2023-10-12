using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public static BulletFactory Instance { get; private set; }

    [SerializeField] Bullet _bulletPrefab;
    [SerializeField] int _initialAmount;

    Pool<Bullet> _myBulletPool;

    void Awake()
    {

        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }

        _myBulletPool = new Pool<Bullet>(CreatorMethod, Bullet.TurnOn, Bullet.TurnOff, _initialAmount);
    }

    private Bullet CreatorMethod()
    {
        return Instantiate(_bulletPrefab);
    }

    public Bullet GetObjectFromPool()
    {
        return _myBulletPool.GetObject();
    }

    public void ReturnToPool(Bullet obj)
    {
        _myBulletPool.ReturnObject(obj);
    }
}
