using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _game;

    private void Start()
    {
        Cursor.visible = false;
        ScreenManagerDefault.Instance.Push(new ScreenGameplay(_game));
        SaveManager.Save();
    }

    public static void Stop()
    {
        if (ScreenManagerDefault.Instance.ScreenCount == 1)
        {
            var screenPause = Instantiate(Resources.Load<ScreenPause>("Canvas Pause"));
            ScreenManagerDefault.Instance.Push(screenPause);
            Cursor.visible = true;
        }
        else if (ScreenManagerDefault.Instance.ScreenCount>1)
        {
            ScreenManagerDefault.Instance.Pop();
        }        
    }
}
