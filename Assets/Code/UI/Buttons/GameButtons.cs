using System.Collections;
using UnityEngine;

public class GameButtons : UIButton
{
    [SerializeField]
    private StateMachine.GameButtonType buttonType;

    protected override void DoButtonStaff()
    {
        switch (buttonType)
        {
            case StateMachine.GameButtonType.ReturnToMenu:
                StateBus.GameStateChanged += StateMachine.GameStates.Menu;
                break;
            case StateMachine.GameButtonType.RestartGame:
                StateBus.GameStateChanged += StateMachine.GameStates.Game;
                break;
        }
    }
}
