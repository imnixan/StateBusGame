using System.Collections;
using UnityEngine;

public class MenuButton : UIButton
{
    [SerializeField]
    private StateMachine.MenuButtonType buttonType;

    protected override void DoButtonStaff()
    {
        switch (buttonType)
        {
            case StateMachine.MenuButtonType.ExitButton:
                ExitGame();
                break;
            case StateMachine.MenuButtonType.StartButton:
                StartGame();
                break;
        }
    }

    private void ExitGame()
    {
        Debug.Log("Close App");
        Application.Quit();
    }

    private void StartGame()
    {
        StateBus.GameStateChanged += StateMachine.GameStates.Game;
    }
}
