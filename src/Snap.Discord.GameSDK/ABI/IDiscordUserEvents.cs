namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordUserEvents
{
    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> on_current_user_update;
}
