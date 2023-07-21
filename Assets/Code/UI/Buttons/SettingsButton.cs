using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : UIButton
{
    private Sprite onSprite,
        offSprite;
    private string PlayerPrefsString;
    private Image buttonImage;
    private string currentStatus;

    protected override void Start()
    {
        base.Start();

        InitSettingsButton();
    }

    private void InitSettingsButton()
    {
        buttonImage = GetComponent<Image>();
        InitialPrefString();
        currentStatus = PlayerPrefs.GetString(
            PlayerPrefsString,
            StaticConstants.TurnedOnSettingsValue
        );
        InitialSprites();
        SetSprite();
    }

    private void InitialPrefString()
    {
        switch (buttonType)
        {
            case StateMachine.ButtonType.SoundButton:
                PlayerPrefsString = StaticConstants.SoundSettingsPrefs;
                break;
            case StateMachine.ButtonType.VibroButton:
                PlayerPrefsString = StaticConstants.VibroSettingsPrefs;
                break;
        }
    }

    private void InitialSprites()
    {
        onSprite = Resources.Load<Sprite>(
            $"SettingsButtons/{PlayerPrefsString}{StaticConstants.TurnedOnSettingsValue}"
        );
        offSprite = Resources.Load<Sprite>(
            $"SettingsButtons/{PlayerPrefsString}{StaticConstants.TurnedOffSettingsVaule}"
        );
    }

    private void SetSprite()
    {
        if (currentStatus == StaticConstants.TurnedOnSettingsValue)
        {
            buttonImage.sprite = onSprite;
        }
        else
        {
            buttonImage.sprite = offSprite;
        }
    }

    private void UpdateSettings()
    {
        currentStatus = TurnedOn()
            ? StaticConstants.TurnedOffSettingsVaule
            : StaticConstants.TurnedOnSettingsValue;
        PlayerPrefs.SetString(PlayerPrefsString, currentStatus);
        PlayerPrefs.Save();
        SetSprite();
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
