namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordRect
{
    [NativeTypeName("int32_t")]
    public int left;

    [NativeTypeName("int32_t")]
    public int top;

    [NativeTypeName("int32_t")]
    public int right;

    [NativeTypeName("int32_t")]
    public int bottom;
}
