namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordActivityManager
{
    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordActivityManager *, const char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordActivityManager*, sbyte*, DiscordResult> register_command;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordActivityManager *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordActivityManager*, uint, DiscordResult> register_steam;

    [NativeTypeName("void (*)(struct IDiscordActivityManager *, struct DiscordActivity *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordActivityManager*, DiscordActivity*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> update_activity;

    [NativeTypeName("void (*)(struct IDiscordActivityManager *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordActivityManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> clear_activity;

    [NativeTypeName("void (*)(struct IDiscordActivityManager *, DiscordUserId, enum EDiscordActivityJoinRequestReply, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordActivityManager*, long, DiscordActivityJoinRequestReply, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> send_request_reply;

    [NativeTypeName("void (*)(struct IDiscordActivityManager *, DiscordUserId, enum EDiscordActivityActionType, const char *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordActivityManager*, long, DiscordActivityActionType, sbyte*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> send_invite;

    [NativeTypeName("void (*)(struct IDiscordActivityManager *, DiscordUserId, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordActivityManager*, long, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> accept_invite;
}
