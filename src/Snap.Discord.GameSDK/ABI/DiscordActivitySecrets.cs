namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordActivitySecrets
{
    [NativeTypeName("char[128]")]
    public fixed sbyte match[128];

    [NativeTypeName("char[128]")]
    public fixed sbyte join[128];

    [NativeTypeName("char[128]")]
    public fixed sbyte spectate[128];
}
