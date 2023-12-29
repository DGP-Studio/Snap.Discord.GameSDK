namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordNetworkEvents
{
    [NativeTypeName("void (*)(void *, DiscordNetworkPeerId, DiscordNetworkChannelId, uint8_t *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, ulong, byte, byte*, uint, void> on_message;

    [NativeTypeName("void (*)(void *, const char *)")]
    public delegate* unmanaged[Cdecl]<void*, sbyte*, void> on_route_update;
}
