namespace Snap.Discord.GameSDK;

public enum CreateFlags : ulong
{
    /// <summary>
    /// Requires Discord to be running to play the game
    /// </summary>
    Default = 0,

    /// <summary>
    /// Does not require Discord to be running, use this on other platforms
    /// </summary>
    NoRequireDiscord = 1,
}