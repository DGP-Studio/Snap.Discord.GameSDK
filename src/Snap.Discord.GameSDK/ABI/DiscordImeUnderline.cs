namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordImeUnderline
{
    [NativeTypeName("int32_t")]
    public int from;

    [NativeTypeName("int32_t")]
    public int to;

    [NativeTypeName("uint32_t")]
    public uint color;

    [NativeTypeName("uint32_t")]
    public uint background_color;

    [NativeTypeName("bool")]
    public byte thick;
}
