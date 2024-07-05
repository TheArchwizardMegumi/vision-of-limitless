public static class MsgType
{
    public static string SimpleEvent = nameof(SimpleEvent);
    public static string IntEvent = nameof(IntEvent);

    // in-game events
    public static string ChangeGold = nameof(ChangeGold);
    public static string ChangeHealth = nameof(ChangeHealth);
    public static string ChangeWaves = nameof(ChangeWaves);

    public static string GameOver = nameof(GameOver);
    public static string GameWin = nameof(GameWin);
}