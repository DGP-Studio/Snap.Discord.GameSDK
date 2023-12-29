namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordOverlayManager
{
    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, bool *)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, bool*, void> is_enabled;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, bool *)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, bool*, void> is_locked;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, bool, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, byte, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> set_locked;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, enum EDiscordActivityActionType, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, DiscordActivityActionType, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> open_activity_invite;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, const char *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, sbyte*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> open_guild_invite;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> open_voice_settings;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordOverlayManager *, IDXGISwapChain *, bool)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, void*, byte, DiscordResult> init_drawing_dxgi;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, void> on_present;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, MSG *)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, void*, void> forward_message;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, bool, const char *, enum EDiscordKeyVariant)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, byte, sbyte*, DiscordKeyVariant, void> key_event;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, const char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, sbyte*, void> char_event;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, uint8_t, int32_t, enum EDiscordMouseButton, int32_t, int32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, byte, int, DiscordMouseButton, int, int, void> mouse_button_event;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, int32_t, int32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, int, int, void> mouse_motion_event;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, const char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, sbyte*, void> ime_commit_text;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, const char *, struct DiscordImeUnderline *, uint32_t, int32_t, int32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, sbyte*, DiscordImeUnderline*, uint, int, int, void> ime_set_composition;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, void> ime_cancel_composition;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, void *, void (*)(void *, int32_t, int32_t, struct DiscordRect *, uint32_t))")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, void*, delegate* unmanaged[Cdecl]<void*, int, int, DiscordRect*, uint, void>, void> set_ime_composition_range_callback;

    [NativeTypeName("void (*)(struct IDiscordOverlayManager *, void *, void (*)(void *, struct DiscordRect, struct DiscordRect, bool))")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordRect, DiscordRect, byte, void>, void> set_ime_selection_bounds_callback;

    [NativeTypeName("bool (*)(struct IDiscordOverlayManager *, int32_t, int32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordOverlayManager*, int, int, byte> is_point_inside_click_zone;
}
