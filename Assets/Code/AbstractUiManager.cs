using System.Collections;
using UnityEngine;

public abstract class AbstractUiManager : AbstractManager
{
    protected UIButton[] buttons;

    protected void Start()
    {
        buttons = GetComponentsInChildren<UIButton>();
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
