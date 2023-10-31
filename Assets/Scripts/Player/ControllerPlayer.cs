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
        }
        if (Input.GetButtonDown("Jump"))
        {
            _visual.PlayJumpParticles();
        }
    }

    public void UpdateInput()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SceneManagement.ReloadScene();
        }
    }
}
