using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] Transform _mainGame;
    
    [SerializeField] Transform _miniGame;

    private void Start()
    {
        //Llamar al screenManager para asignarle la pantalla principal (pushear el mainGame)
        ScreenManagerDefault.Instance.Push(new ScreenGameplay(_mainGame));

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Pusheamos el minigame
            ScreenManagerDefault.Instance.Push(new ScreenGameplay(Instantiate(_miniGame)));
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            //Pusheamos el menu de pausa
            var screenPause = Instantiate(Resources.Load<ScreenPause>("Canvas Pause"));
            ScreenManagerDefault.Instance.Push(screenPause);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Sacamos la ultima pantalla habilitada
            ScreenManagerDefault.Instance.Pop();
        }
    }
}
