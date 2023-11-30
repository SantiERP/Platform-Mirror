using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _game;
    public static GameManager Instance { get; set; }

    public bool pause;

    public delegate void Pause();
    public Pause _pause; 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    private void Start()
    {
        ScreenManagerDefault.Instance.Push(new ScreenGameplay(_game));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            var screenPause = Instantiate(Resources.Load<ScreenPause>("Canvas Pause"));
            ScreenManagerDefault.Instance.Push(screenPause);
            pause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            pause = false;
            ScreenManagerDefault.Instance.Pop();
        }
    }
}
