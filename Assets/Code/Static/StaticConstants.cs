public static class StaticConstants
{
    private static int _turnedOnSettingsVaule = 1;
    private static int _turnedOffSettingsVaule = 0;
    private static string _vibroSettingsPrefs = "VIBRO";
    private static string _soundSettingsPrefs = "SOUND";

    public static int TurnedOnSettingsValue
    {
        get { return _turnedOnSettingsVaule; }
    }

    public static int TurnedOffSettingsVaule
    {
        get { return _turnedOffSettingsVaule; }
    }

    public static string VibroSettingsPrefs
    {
        get { return _vibroSettingsPrefs; }
    }

    public static string SoundSettingsPrefs
    {
        get { return _soundSettingsPrefs; }
    }
}
