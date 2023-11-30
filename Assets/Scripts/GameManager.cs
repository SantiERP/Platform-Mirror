using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _game;

    public bool Pause;

    // Update is called once per frame
    private void Start()
    {
        ScreenManagerDefault.Instance.Push(new ScreenGameplay(_game));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Pause)
        {
            var screenPause = Instantiate(Resources.Load<ScreenPause>("Canvas Pause"));
            ScreenManagerDefault.Instance.Push(screenPause);
            Pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Pause)
        {
            Pause = false;
            ScreenManagerDefault.Instance.Pop();
        }
    }
}
