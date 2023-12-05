using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _game;


    private void Start()
    {
        ScreenManagerDefault.Instance.Push(new ScreenGameplay(_game));
        SaveManager.Save();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && ScreenManagerDefault.Instance.ScreenCount == 1)
        {
            var screenPause = Instantiate(Resources.Load<ScreenPause>("Canvas Pause"));
            ScreenManagerDefault.Instance.Push(screenPause);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ScreenManagerDefault.Instance.ScreenCount>1)
        {
            ScreenManagerDefault.Instance.Pop();
        }
    }
}
