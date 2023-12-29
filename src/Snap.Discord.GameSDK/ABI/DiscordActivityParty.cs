namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordActivityParty
{
    [NativeTypeName("char[128]")]
    public fixed sbyte id[128];

    [NativeTypeName("struct DiscordPartySize")]
    public DiscordPartySize size;

    [NativeTypeName("enum EDiscordActivityPartyPrivacy")]
    public DiscordActivityPartyPrivacy privacy;
}
