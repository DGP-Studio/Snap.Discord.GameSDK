namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordActivityAssets
{
    [NativeTypeName("char[128]")]
    public fixed sbyte large_image[128];

    [NativeTypeName("char[128]")]
    public fixed sbyte large_text[128];

    [NativeTypeName("char[128]")]
    public fixed sbyte small_image[128];

    [NativeTypeName("char[128]")]
    public fixed sbyte small_text[128];
}
