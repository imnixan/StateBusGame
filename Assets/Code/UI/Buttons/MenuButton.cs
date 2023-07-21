using System.Collections;
using UnityEngine;

public class MenuButton : UIButton
{
    protected override void DoButtonStaff()
    {
        switch (buttonType)
        {
            case StateMachine.ButtonType.ExitButton:
                ExitGame();
                break;
            case StateMachine.ButtonType.StartButton:
                StartGame();
                break;
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        StateBus.GameStateChanged += StateMachine.GameStates.Game;
    }
}
