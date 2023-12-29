namespace Snap.Discord.GameSDK.ABI;

public partial struct DiscordRelationship
{
    [NativeTypeName("enum EDiscordRelationshipType")]
    public DiscordRelationshipType type;

    [NativeTypeName("struct DiscordUser")]
    public DiscordUser user;

    [NativeTypeName("struct DiscordPresence")]
    public DiscordPresence presence;
}
