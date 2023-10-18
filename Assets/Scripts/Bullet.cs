using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _maxLifeTime;
    [SerializeField, Range(1,50)] float _speed;
    float _lifeTime;

    void Awake()
    {
        _lifeTime = _maxLifeTime;
    }

    void Update()
    {
        _lifeTime -= Time.deltaTime;
        transform.position += transform.right * -1 * _speed * Time.deltaTime;
        if (_lifeTime <= 0)
        {
            BulletFactory.Instance.ReturnToPool(this);
        }
    }

    public void Reset()
    {
        _lifeTime = _maxLifeTime;
    }

    public static void TurnOn(Bullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.TryGetComponent<ModelPlayer>(out ModelPlayer player))
       {
            EventManager.TriggerEvent(EventManager.EventsType.Event_PlayerDead);
       }else BulletFactory.Instance.ReturnToPool(this);

    }

}
