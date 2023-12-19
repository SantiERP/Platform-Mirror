using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Creditos : MonoBehaviour
{
    int Speed = 5;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        RectTransform rect = GetComponent<RectTransform>();
        WaitForSeconds wait = new WaitForSeconds(0.01f);

            while(rect.anchoredPosition.y < 480)
            {
                rect.anchoredPosition += Vector2.up * Speed;
                yield return wait;
            }

        Application.Quit();
    }
}
