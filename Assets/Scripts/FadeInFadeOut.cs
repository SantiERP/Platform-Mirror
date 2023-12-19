using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class FadeInFadeOut : MonoBehaviour
{
    int Speed = 50;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        if (rect.anchoredPosition.y != 0)
        {
            while(rect.anchoredPosition.y < 0)
            {
                rect.anchoredPosition += Vector2.up * Speed;
                yield return wait;
            }
            
            DontDestroyOnLoad(gameObject);
            SaveManager.RestartSaveManager();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        while (rect.anchoredPosition.y < 1920)
        {
            rect.anchoredPosition += Vector2.up * Speed;
            yield return wait;
        }

        Destroy(gameObject);
    }
}
