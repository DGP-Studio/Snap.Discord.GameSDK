namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordUser
{
    [NativeTypeName("DiscordUserId")]
    public long id;

    [NativeTypeName("char[256]")]
    public fixed sbyte username[256];

    [NativeTypeName("char[8]")]
    public fixed sbyte discriminator[8];

    [NativeTypeName("char[128]")]
    public fixed sbyte avatar[128];

    [NativeTypeName("bool")]
    public byte bot;
}
