using UnityEngine;

public class TurretShot : MonoBehaviour
{
    [SerializeField] float _cooldown;
    [SerializeField] float _distanceRay;

    [SerializeField] LayerMask _layerMask;
    float timer;
    Bullet b;

    private void Awake()
    {
        timer = _cooldown;
    }
    void Update()
    {
        if (!Physics.Raycast(transform.position, transform.right * -1, _distanceRay, _layerMask)) { Debug.Log("Linea de fuego"); return; }

        if (timer < 0)
        {
            b = BulletFactory.Instance.GetObjectFromPool();
            b.transform.position = transform.position;
            timer = _cooldown;
        }
        timer -= Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.right * -_distanceRay);
    }
}
