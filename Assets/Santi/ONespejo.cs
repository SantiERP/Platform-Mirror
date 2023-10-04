using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONespejo : MonoBehaviour
{
    [SerializeField] EspejoAntigravedad espejo;
    [SerializeField] GameObject cubo;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Personaje personaje = other.GetComponent<Personaje>();
        if (personaje)
        {
            if(espejo) espejo.gameObject.SetActive(false);

            cubo.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
