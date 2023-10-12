using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONespejo : MonoBehaviour
{
    [SerializeField] AntigravityMirror espejo;
    [SerializeField] GameObject cubo;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Player personaje = other.GetComponent<Player>();
        if (personaje)
        {
            if(espejo) espejo.gameObject.SetActive(false);

            cubo.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
