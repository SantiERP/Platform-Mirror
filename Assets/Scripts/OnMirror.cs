using System.Collections.Generic;
using UnityEngine;

public class OnMirror : MonoBehaviour
{
    [SerializeField] List<GameObject> _objects;

    private void OnTriggerEnter(Collider other)
    {
        Player _character = other.GetComponent<Player>();
        if (_character)
        {
            foreach(GameObject i in _objects)
            {
                if (i == null) continue;
                i.SetActive(!i.activeSelf);

                if(i.transform.parent == this.transform)
                {
                    i.transform.parent = null;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
