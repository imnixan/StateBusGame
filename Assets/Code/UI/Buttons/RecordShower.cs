using UnityEngine;
using TMPro;

public class RecordShower : GUIElement
{
    private TextMeshProUGUI record;

    protected override void Start()
    {
        record = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void ShowButton()
    {
        base.ShowButton();
        record.text = PlayerPrefs.GetInt("Record").ToString();
    }
}
