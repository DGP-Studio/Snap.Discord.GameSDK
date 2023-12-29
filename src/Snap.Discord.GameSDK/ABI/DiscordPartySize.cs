namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordPartySize
{
    [NativeTypeName("int32_t")]
    public int current_size;

    [NativeTypeName("int32_t")]
    public int max_size;
}
