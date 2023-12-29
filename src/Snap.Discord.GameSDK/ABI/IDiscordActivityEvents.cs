namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordActivityEvents
{
    [NativeTypeName("void (*)(void *, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, sbyte*, void> on_activity_join;

    [NativeTypeName("void (*)(void *, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, sbyte*, void> on_activity_spectate;

    [NativeTypeName("void (*)(void *, struct DiscordUser *)")]
    public delegate* unmanaged[Cdecl]<void*, DiscordUser*, void> on_activity_join_request;

    [NativeTypeName("void (*)(void *, enum EDiscordActivityActionType, struct DiscordUser *, struct DiscordActivity *)")]
    public delegate* unmanaged[Cdecl]<void*, DiscordActivityActionType, DiscordUser*, DiscordActivity*, void> on_activity_invite;
}
