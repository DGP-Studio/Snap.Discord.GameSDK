namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordVoiceManager
{
    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, struct DiscordInputMode *)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, DiscordInputMode*, DiscordResult> get_input_mode;

    [NativeTypeName("void (*)(struct IDiscordVoiceManager *, struct DiscordInputMode, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, DiscordInputMode, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> set_input_mode;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, bool *)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, bool*, DiscordResult> is_self_mute;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, bool)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, byte, DiscordResult> set_self_mute;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, bool *)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, bool*, DiscordResult> is_self_deaf;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, bool)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, byte, DiscordResult> set_self_deaf;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, DiscordSnowflake, bool *)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, long, bool*, DiscordResult> is_local_mute;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, DiscordSnowflake, bool)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, long, byte, DiscordResult> set_local_mute;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, DiscordSnowflake, uint8_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, long, byte*, DiscordResult> get_local_volume;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordVoiceManager *, DiscordSnowflake, uint8_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordVoiceManager*, long, byte, DiscordResult> set_local_volume;
}
