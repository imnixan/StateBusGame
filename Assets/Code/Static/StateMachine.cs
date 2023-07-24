public class StateMachine
{
    public enum GameStates
    {
        Null,
        Menu,
        Game,
        GameEnd
    }

    public enum MenuButtonType
    {
        StartButton,
        ExitButton
    }

    public enum SettingsButtonType
    {
        VibroButton,
        SoundButton
    }

    public enum GameButtonType
    {
        ReturnToMenu,
        RestartGame
    }
}
