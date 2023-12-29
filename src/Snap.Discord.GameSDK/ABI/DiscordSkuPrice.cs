namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordSkuPrice
{
    [NativeTypeName("uint32_t")]
    public uint amount;

    [NativeTypeName("char[16]")]
    public fixed sbyte currency[16];
}
