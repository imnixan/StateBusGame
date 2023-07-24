using System.Collections;
using UnityEngine;

public abstract class AbstractUiManager : AbstractManager
{
    protected Hideable[] buttons;

    protected void Start()
    {
        buttons = GetComponentsInChildren<Hideable>();
    }

    protected void HideButtons()
    {
        foreach (var button in buttons)
        {
            button.HideButton();
        }
    }

    protected void ShowButtons()
    {
        foreach (var button in buttons)
        {
            button.ShowButton();
        }
    }
}
