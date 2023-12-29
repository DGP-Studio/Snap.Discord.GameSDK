namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordUserManager
{
    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordUserManager *, struct DiscordUser *)")]
    public delegate* unmanaged[Cdecl]<IDiscordUserManager*, DiscordUser*, DiscordResult> get_current_user;

    [NativeTypeName("void (*)(struct IDiscordUserManager *, DiscordUserId, void *, void (*)(void *, enum EDiscordResult, struct DiscordUser *))")]
    public delegate* unmanaged[Cdecl]<IDiscordUserManager*, long, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, DiscordUser*, void>, void> get_user;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordUserManager *, enum EDiscordPremiumType *)")]
    public delegate* unmanaged[Cdecl]<IDiscordUserManager*, DiscordPremiumType*, DiscordResult> get_current_user_premium_type;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordUserManager *, enum EDiscordUserFlag, bool *)")]
    public delegate* unmanaged[Cdecl]<IDiscordUserManager*, DiscordUserFlag, bool*, DiscordResult> current_user_has_flag;
}
