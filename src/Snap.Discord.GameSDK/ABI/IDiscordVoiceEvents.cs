namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordVoiceEvents
{
    [NativeTypeName("void (*)(void *)")]
    public delegate* unmanaged[Cdecl]<void*, void> on_settings_update;
}
