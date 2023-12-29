namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordImageDimensions
{
    [NativeTypeName("uint32_t")]
    public uint width;

    [NativeTypeName("uint32_t")]
    public uint height;
}
