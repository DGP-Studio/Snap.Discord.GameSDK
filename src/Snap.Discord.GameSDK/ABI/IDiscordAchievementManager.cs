namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordAchievementManager
{
    [NativeTypeName("void (*)(struct IDiscordAchievementManager *, DiscordSnowflake, uint8_t, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordAchievementManager*, long, byte, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> set_user_achievement;

    [NativeTypeName("void (*)(struct IDiscordAchievementManager *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordAchievementManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> fetch_user_achievements;

    [NativeTypeName("void (*)(struct IDiscordAchievementManager *, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordAchievementManager*, int*, void> count_user_achievements;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordAchievementManager *, DiscordSnowflake, struct DiscordUserAchievement *)")]
    public delegate* unmanaged[Cdecl]<IDiscordAchievementManager*, long, DiscordUserAchievement*, DiscordResult> get_user_achievement;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordAchievementManager *, int32_t, struct DiscordUserAchievement *)")]
    public delegate* unmanaged[Cdecl]<IDiscordAchievementManager*, int, DiscordUserAchievement*, DiscordResult> get_user_achievement_at;
}
