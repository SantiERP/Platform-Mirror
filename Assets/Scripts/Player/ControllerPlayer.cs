using UnityEngine;

public class ControllerPlayer : IController
{
    VisualPlayer _visual;

    ModelPlayer jugador;

    public ControllerPlayer(VisualPlayer visual, ModelPlayer jugador)
    {
        _visual = visual;
        this.jugador = jugador;
    }

    public void FixedUpdateInput()
    {
        jugador.NormalMove(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump"))
        {
            jugador.Salto();
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
