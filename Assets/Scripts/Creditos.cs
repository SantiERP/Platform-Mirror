using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Creditos : MonoBehaviour
{
    int Speed = 2;
    TMP_Text text;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        text = GetComponent<TMP_Text>();
        RectTransform rect = GetComponent<RectTransform>();
        WaitForSeconds wait = new WaitForSeconds(0.01f);

            while(rect.anchoredPosition.y < 480)
            {
                rect.anchoredPosition += Vector2.up * Speed;
                yield return wait;
            }

        float repetitions = 0;
        while(text.color != Color.clear)
        {
            text.color = Color.Lerp(Color.white, Color.clear, repetitions);
            repetitions += 0.01f;
            yield return wait;
        }

        Application.Quit();
    }
}
