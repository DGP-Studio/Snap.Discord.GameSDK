namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordActivityTimestamps
{
    [NativeTypeName("DiscordTimestamp")]
    public long start;

    [NativeTypeName("DiscordTimestamp")]
    public long end;
}
