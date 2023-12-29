namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordOverlayEvents
{
    [NativeTypeName("void (*)(void *, bool)")]
    public delegate* unmanaged[Cdecl]<void*, byte, void> on_toggle;
}
