namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordOAuth2Token
{
    [NativeTypeName("char[128]")]
    public fixed sbyte access_token[128];

    [NativeTypeName("char[1024]")]
    public fixed sbyte scopes[1024];

    [NativeTypeName("DiscordTimestamp")]
    public long expires;
}
