using UnityEngine;

public class TurretShot : MonoBehaviour
{
    [SerializeField] float _cooldown;
    [SerializeField] float _distanceRay;

    [SerializeField] LayerMask _layerMask;
    float _timer;
    Bullet _bullet;

    private void Awake()
    {
        _timer = _cooldown;
    }
    void Update()
    {
        if (!Physics.Raycast(transform.position, transform.right * -1, _distanceRay, _layerMask)) { Debug.Log("Linea de fuego"); return; }

        if (_timer < 0)
        {
            _bullet = BulletFactory.Instance.GetObjectFromPool();
            _bullet.transform.position = transform.position;
            _timer = _cooldown;
        }
        _timer -= Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.right * -_distanceRay);
    }
}
