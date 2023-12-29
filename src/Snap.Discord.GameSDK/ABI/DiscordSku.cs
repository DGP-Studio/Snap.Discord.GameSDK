namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordSku
{
    [NativeTypeName("DiscordSnowflake")]
    public long id;

    [NativeTypeName("enum EDiscordSkuType")]
    public DiscordSkuType type;

    [NativeTypeName("char[256]")]
    public fixed sbyte name[256];

    [NativeTypeName("struct DiscordSkuPrice")]
    public DiscordSkuPrice price;
}
