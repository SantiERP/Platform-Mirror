using UnityEngine;

public class ControllerPlayer : IController
{
    VisualPlayer _visual;

    ModelPlayer _player;

    public ControllerPlayer(VisualPlayer visual, ModelPlayer jugador)
    {
        _visual = visual;
        this._player = jugador;
    }

    public void FixedUpdateInput()
    {
        _player.NormalMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump"))
        {
            _player.Jump();
            Debug.Log("Salta");
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(_player.TouchingTheFloor())
            _visual.Jump();
        }
    }

    public void UpdateInput()
    {
        if (Input.GetButtonDown("Restart"))
        {
            foreach(Mirrors mirror in Mirrors.allMirrors)
            {
                if (mirror == null) continue;
                mirror.StopAllCoroutines();
                mirror.UnconstrainAllRigs();
            }

            SaveManager.Remember();
            Debug.Log("Remembering");
        }

        if (Input.GetButtonUp("Jump"))
        {
            _player.StopJumping();
        }
    }
}
