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
            _visual.PlayJumpParticles();
        }
    }

    public void UpdateInput()
    {
        if (Input.GetButtonDown("Restart"))
        {
            SaveManager.Remember();
            Debug.Log("Remembering");
        }
    }
}
