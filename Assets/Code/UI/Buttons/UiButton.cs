using UnityEngine;
using System.Collections;

public abstract class UIButton : ExplodableObject
{
    protected override void Start()
    {
        base.Start();
        Init();
        gameObject.tag = "Button";
    }

    public override void ShowButton()
    {
        base.ShowButton();
        collider.enabled = true;
    }

    public void HideButton()
    {
        base.HideButton();
        collider.enabled = false;
    }

    public override void Init()
    {
        base.Init();
        rt = GetComponent<RectTransform>();
    }

    protected abstract void DoButtonStaff();

    protected override void OnExplode()
    {
        DoButtonStaff();
    }
}
