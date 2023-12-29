namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordApplicationManager
{
    [NativeTypeName("void (*)(struct IDiscordApplicationManager *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordApplicationManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> validate_or_exit;

    [NativeTypeName("void (*)(struct IDiscordApplicationManager *, DiscordLocale *)")]
    public delegate* unmanaged[Cdecl]<IDiscordApplicationManager*, sbyte*, void> get_current_locale;

    [NativeTypeName("void (*)(struct IDiscordApplicationManager *, DiscordBranch *)")]
    public delegate* unmanaged[Cdecl]<IDiscordApplicationManager*, sbyte*, void> get_current_branch;

    [NativeTypeName("void (*)(struct IDiscordApplicationManager *, void *, void (*)(void *, enum EDiscordResult, struct DiscordOAuth2Token *))")]
    public delegate* unmanaged[Cdecl]<IDiscordApplicationManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, DiscordOAuth2Token*, void>, void> get_oauth2_token;

    [NativeTypeName("void (*)(struct IDiscordApplicationManager *, void *, void (*)(void *, enum EDiscordResult, const char *))")]
    public delegate* unmanaged[Cdecl]<IDiscordApplicationManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, sbyte*, void>, void> get_ticket;
}
