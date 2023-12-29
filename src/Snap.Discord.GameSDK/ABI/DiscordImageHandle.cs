namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordImageHandle
{
    [NativeTypeName("enum EDiscordImageType")]
    public DiscordImageType type;

    [NativeTypeName("int64_t")]
    public long id;

    [NativeTypeName("uint32_t")]
    public uint size;
}
