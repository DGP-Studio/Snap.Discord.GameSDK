namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordLobbyTransaction
{
    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyTransaction *, enum EDiscordLobbyType)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyTransaction*, DiscordLobbyType, DiscordResult> set_type;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyTransaction *, DiscordUserId)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyTransaction*, long, DiscordResult> set_owner;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyTransaction *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyTransaction*, uint, DiscordResult> set_capacity;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyTransaction *, char *, char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyTransaction*, sbyte, sbyte, DiscordResult> set_metadata;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyTransaction *, char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyTransaction*, sbyte, DiscordResult> delete_metadata;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyTransaction *, bool)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyTransaction*, byte, DiscordResult> set_locked;
}
