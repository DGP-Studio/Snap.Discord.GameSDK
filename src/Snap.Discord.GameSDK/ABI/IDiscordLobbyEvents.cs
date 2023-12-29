namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordLobbyEvents
{
    [NativeTypeName("void (*)(void *, int64_t)")]
    public delegate* unmanaged[Cdecl]<void*, long, void> on_lobby_update;

    [NativeTypeName("void (*)(void *, int64_t, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, long, uint, void> on_lobby_delete;

    [NativeTypeName("void (*)(void *, int64_t, int64_t)")]
    public delegate* unmanaged[Cdecl]<void*, long, long, void> on_member_connect;

    [NativeTypeName("void (*)(void *, int64_t, int64_t)")]
    public delegate* unmanaged[Cdecl]<void*, long, long, void> on_member_update;

    [NativeTypeName("void (*)(void *, int64_t, int64_t)")]
    public delegate* unmanaged[Cdecl]<void*, long, long, void> on_member_disconnect;

    [NativeTypeName("void (*)(void *, int64_t, int64_t, uint8_t *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, long, long, byte*, uint, void> on_lobby_message;

    [NativeTypeName("void (*)(void *, int64_t, int64_t, bool)")]
    public delegate* unmanaged[Cdecl]<void*, long, long, byte, void> on_speaking;

    [NativeTypeName("void (*)(void *, int64_t, int64_t, uint8_t, uint8_t *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<void*, long, long, byte, byte*, uint, void> on_network_message;
}
