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

    /// <summary>
    /// 切换睁眼闭眼时触发的事件
    /// </summary>
    public static string changeOpenCloseEye = nameof(changeOpenCloseEye);
    /// <summary>
    /// 玩家受伤时触发的事件
    /// </summary>
    public static string playerHert = nameof(playerHert);
}