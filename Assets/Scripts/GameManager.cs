using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _game;
    bool pause;
    // Update is called once per frame
    private void Start()
    {
        //ScreenManager.Instance.Push(new ScreenGameplay(_game));
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            var screenPause = Instantiate(Resources.Load<ScreenPause>("Canvas Pause"));
            ScreenManager.Instance.Push(screenPause);
            pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            pause = false;
            ScreenManager.Instance.Pop();
        }
    }
}
