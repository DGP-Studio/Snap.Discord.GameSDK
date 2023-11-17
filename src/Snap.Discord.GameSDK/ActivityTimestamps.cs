namespace Snap.Discord.GameSDK;

public struct ActivityTimestamps
{
    /// <summary>
    /// unix timestamp - send this to have an "elapsed" timer
    /// </summary>
    public long Start;

    /// <summary>
    /// unix timestamp - send this to have a "remaining" timer
    /// </summary>
    public long End;
}