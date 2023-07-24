using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : UIButton
{
    [SerializeField]
    private Color green,
        greenStroke,
        red,
        redStroke;

    [SerializeField]
    private StateMachine.SettingsButtonType buttonType;

    private string PlayerPrefsString;
    private Image buttonImage,
        buttonFace;
    private int currentStatus;

    protected override void Start()
    {
        base.Start();

        InitSettingsButton();
    }

    private void InitSettingsButton()
    {
        buttonImage = GetComponent<Image>();
        buttonFace = transform.GetChild(0).GetComponent<Image>();
        InitialPrefString();
        currentStatus = PlayerPrefs.GetInt(
            PlayerPrefsString,
            StaticConstants.TurnedOnSettingsValue
        );
        SetColors();
    }

    private void InitialPrefString()
    {
        switch (buttonType)
        {
            case StateMachine.SettingsButtonType.SoundButton:
                PlayerPrefsString = StaticConstants.SoundSettingsPrefs;
                break;
            case StateMachine.SettingsButtonType.VibroButton:
                PlayerPrefsString = StaticConstants.VibroSettingsPrefs;
                break;
        }
    }

    private void SetColors()
    {
        if (currentStatus == StaticConstants.TurnedOnSettingsValue)
        {
            buttonImage.color = greenStroke;
            buttonFace.color = green;
        }
        else
        {
            buttonImage.color = redStroke;
            buttonFace.color = red;
        }
    }

    private void UpdateSettings()
    {
        currentStatus = TurnedOn()
            ? StaticConstants.TurnedOffSettingsVaule
            : StaticConstants.TurnedOnSettingsValue;
        PlayerPrefs.SetInt(PlayerPrefsString, currentStatus);
        PlayerPrefs.Save();
        SetColors();
        StateBus.SettingsChanged += true;
    }

    private bool TurnedOn()
    {
        return currentStatus == StaticConstants.TurnedOnSettingsValue;
    }

    protected override void DoButtonStaff()
    {
        UpdateSettings();
    }
}
