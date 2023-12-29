namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct DiscordUserAchievement
{
    [NativeTypeName("DiscordSnowflake")]
    public long user_id;

    [NativeTypeName("DiscordSnowflake")]
    public long achievement_id;

    [NativeTypeName("uint8_t")]
    public byte percent_complete;

    [NativeTypeName("DiscordDateTime")]
    public fixed sbyte unlocked_at[64];
}
