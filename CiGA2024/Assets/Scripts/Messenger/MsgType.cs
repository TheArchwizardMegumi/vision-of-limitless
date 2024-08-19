public static class MsgType
{
    public static string SimpleEvent = nameof(SimpleEvent);
    public static string IntEvent = nameof(IntEvent);

    // in-game events

    public static string GameOver = nameof(GameOver);
    public static string GameWin = nameof(GameWin);
    public static string Walking = nameof(Walking);
    public static string Walking2 = nameof(Walking2);

    /// <summary>
    /// �л����۱���ʱ�������¼�
    /// </summary>
    public static string changeOpenCloseEye = nameof(changeOpenCloseEye);
    /// <summary>
    /// �������ʱ�������¼�
    /// </summary>
    public static string playerHurt = nameof(playerHurt);
    public static string playerWin = nameof(playerWin);
    public static string reachExit = nameof(reachExit);
    public static string levelStart = nameof(levelStart);
}