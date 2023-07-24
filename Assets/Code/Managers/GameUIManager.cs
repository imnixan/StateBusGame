using System.Collections;
using UnityEngine;

public class GameUIManager : AbstractUiManager
{
    public override void OnGameStarted()
    {
        ShowButtons();
    }

    public override void OnMenu()
    {
        HideButtons();
    }
}
