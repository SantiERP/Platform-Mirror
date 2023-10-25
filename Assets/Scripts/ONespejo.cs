using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONespejo : MonoBehaviour
{
    [SerializeField] List<GameObject> Objects;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Player personaje = other.GetComponent<Player>();
        if (personaje)
        {
            foreach(GameObject i in Objects)
            {
                i.SetActive(!i.active);
                }
            Destroy(this.gameObject);
        }
    }
}
