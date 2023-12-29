namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordPresence
{
    [NativeTypeName("enum EDiscordStatus")]
    public DiscordStatus status;

    [NativeTypeName("struct DiscordActivity")]
    public DiscordActivity activity;
}
