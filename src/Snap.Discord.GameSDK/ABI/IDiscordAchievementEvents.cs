namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordAchievementEvents
{
    [NativeTypeName("void (*)(void *, struct DiscordUserAchievement *)")]
    public delegate* unmanaged[Cdecl]<void*, DiscordUserAchievement*, void> on_user_achievement_update;
}
