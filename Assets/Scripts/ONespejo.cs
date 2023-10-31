
using System.Collections.Generic;
using UnityEngine;

public class ONespejo : MonoBehaviour
{
    [SerializeField] List<GameObject> Objects;

    private void OnTriggerEnter(Collider other)
    {
        Player _character = other.GetComponent<Player>();
        if (_character)
        {
            foreach(GameObject i in Objects)
            {
                i.SetActive(!i.active);
                }
            Destroy(this.gameObject);
        }
    }
}
