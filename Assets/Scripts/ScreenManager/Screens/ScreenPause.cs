using UnityEngine;
using UnityEngine.UI;

public class ScreenPause : MonoBehaviour, IScreen
{
    Button[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Button>();
        ActivateButtons(false);
    }

    void ActivateButtons(bool enable)
    {
        foreach (var button in _buttons)
        {
            button.interactable = enable;
        }
    }

    public void BTN_Options()
    {
        var screenVolumeSetting = Instantiate(Resources.Load<ScreenVolumeSetting>("Canvas VolumeSetting"));
        ScreenManagerDefault.Instance.Push(screenVolumeSetting);
        Desactivate();
    }

    public void BTN_Back()
    {
        ScreenManagerDefault.Instance.Pop();
        Cursor.visible = false;
    }
    public void BTN_Quit()
    {
        Application.Quit();
    }
    public void Activate()
    {
        ActivateButtons(true);
    }

    public void Desactivate()
    {
        ActivateButtons(false);
    }

    public void Free()
    {
        Destroy(gameObject);
    }
}
