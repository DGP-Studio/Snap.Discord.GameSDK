namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordEntitlement
{
    [NativeTypeName("DiscordSnowflake")]
    public long id;

    [NativeTypeName("enum EDiscordEntitlementType")]
    public DiscordEntitlementType type;

    [NativeTypeName("DiscordSnowflake")]
    public long sku_id;
}
