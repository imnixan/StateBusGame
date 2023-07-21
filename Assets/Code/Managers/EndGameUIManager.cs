using System.Collections;
using UnityEngine;

public class EndGameUIManager : AbstractUiManager
{
    public override void OnGameStarted()
    {
        HideButtons();
    }

    public override void OnMenu()
    {
        HideButtons();
    }

    public override void OnGameEnded()
    {
        ShowButtons();
    }
}
