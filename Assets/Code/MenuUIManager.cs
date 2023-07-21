using System.Collections;
using UnityEngine;

public class MenuUIManager : AbstractUiManager
{
    public override void OnGameStarted()
    {
        HideButtons();
    }

    public override void OnMenu()
    {
        ShowButtons();
    }
}
