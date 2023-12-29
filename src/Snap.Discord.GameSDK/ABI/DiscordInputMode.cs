namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordInputMode
{
    [NativeTypeName("enum EDiscordInputModeType")]
    public DiscordInputModeType type;

    [NativeTypeName("char[256]")]
    public fixed sbyte shortcut[256];
}
