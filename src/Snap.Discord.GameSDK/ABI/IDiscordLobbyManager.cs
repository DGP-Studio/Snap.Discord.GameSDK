namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordLobbyManager
{
    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, struct IDiscordLobbyTransaction **)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, IDiscordLobbyTransaction**, DiscordResult> get_lobby_create_transaction;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, struct IDiscordLobbyTransaction **)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, IDiscordLobbyTransaction**, DiscordResult> get_lobby_update_transaction;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, DiscordUserId, struct IDiscordLobbyMemberTransaction **)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, long, IDiscordLobbyMemberTransaction**, DiscordResult> get_member_update_transaction;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, struct IDiscordLobbyTransaction *, void *, void (*)(void *, enum EDiscordResult, struct DiscordLobby *))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, IDiscordLobbyTransaction*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, DiscordLobby*, void>, void> create_lobby;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, DiscordLobbyId, struct IDiscordLobbyTransaction *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, IDiscordLobbyTransaction*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> update_lobby;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, DiscordLobbyId, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> delete_lobby;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, DiscordLobbyId, char *, void *, void (*)(void *, enum EDiscordResult, struct DiscordLobby *))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, sbyte, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, DiscordLobby*, void>, void> connect_lobby;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, char *, void *, void (*)(void *, enum EDiscordResult, struct DiscordLobby *))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, sbyte, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, DiscordLobby*, void>, void> connect_lobby_with_activity_secret;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, DiscordLobbyId, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> disconnect_lobby;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, struct DiscordLobby *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, DiscordLobby*, DiscordResult> get_lobby;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, DiscordLobbySecret *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, sbyte*, DiscordResult> get_lobby_activity_secret;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, char *, DiscordMetadataValue *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, sbyte, sbyte*, DiscordResult> get_lobby_metadata_value;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, int32_t, DiscordMetadataKey *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, int, sbyte*, DiscordResult> get_lobby_metadata_key;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, int*, DiscordResult> lobby_metadata_count;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, int*, DiscordResult> member_count;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, int32_t, DiscordUserId *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, int, long*, DiscordResult> get_member_user_id;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, DiscordUserId, struct DiscordUser *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, long, DiscordUser*, DiscordResult> get_member_user;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, DiscordUserId, char *, DiscordMetadataValue *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, long, sbyte, sbyte*, DiscordResult> get_member_metadata_value;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, DiscordUserId, int32_t, DiscordMetadataKey *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, long, int, sbyte*, DiscordResult> get_member_metadata_key;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, DiscordUserId, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, long, int*, DiscordResult> member_metadata_count;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, DiscordLobbyId, DiscordUserId, struct IDiscordLobbyMemberTransaction *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, long, IDiscordLobbyMemberTransaction*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> update_member;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, DiscordLobbyId, uint8_t *, uint32_t, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, byte*, uint, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> send_lobby_message;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, struct IDiscordLobbySearchQuery **)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, IDiscordLobbySearchQuery**, DiscordResult> get_search_query;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, struct IDiscordLobbySearchQuery *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, IDiscordLobbySearchQuery*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> search;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, int*, void> lobby_count;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, int32_t, DiscordLobbyId *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, int, long*, DiscordResult> get_lobby_id;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, DiscordLobbyId, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> connect_voice;

    [NativeTypeName("void (*)(struct IDiscordLobbyManager *, DiscordLobbyId, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> disconnect_voice;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, DiscordResult> connect_network;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, DiscordResult> disconnect_network;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, DiscordResult> flush_network;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, uint8_t, bool)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, byte, byte, DiscordResult> open_network_channel;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyManager *, DiscordLobbyId, DiscordUserId, uint8_t, uint8_t *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyManager*, long, long, byte, byte*, uint, DiscordResult> send_network_message;
}
