namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordNetworkManager
{
    [NativeTypeName("void (*)(struct IDiscordNetworkManager *, DiscordNetworkPeerId *)")]
    public delegate* unmanaged[Cdecl]<IDiscordNetworkManager*, ulong*, void> get_peer_id;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordNetworkManager *)")]
    public delegate* unmanaged[Cdecl]<IDiscordNetworkManager*, DiscordResult> flush;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordNetworkManager *, DiscordNetworkPeerId, const char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordNetworkManager*, ulong, sbyte*, DiscordResult> open_peer;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordNetworkManager *, DiscordNetworkPeerId, const char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordNetworkManager*, ulong, sbyte*, DiscordResult> update_peer;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordNetworkManager *, DiscordNetworkPeerId)")]
    public delegate* unmanaged[Cdecl]<IDiscordNetworkManager*, ulong, DiscordResult> close_peer;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordNetworkManager *, DiscordNetworkPeerId, DiscordNetworkChannelId, bool)")]
    public delegate* unmanaged[Cdecl]<IDiscordNetworkManager*, ulong, byte, byte, DiscordResult> open_channel;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordNetworkManager *, DiscordNetworkPeerId, DiscordNetworkChannelId)")]
    public delegate* unmanaged[Cdecl]<IDiscordNetworkManager*, ulong, byte, DiscordResult> close_channel;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordNetworkManager *, DiscordNetworkPeerId, DiscordNetworkChannelId, uint8_t *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordNetworkManager*, ulong, byte, byte*, uint, DiscordResult> send_message;
}
