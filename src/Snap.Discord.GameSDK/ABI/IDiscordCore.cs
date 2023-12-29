namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordCore
{
    [NativeTypeName("void (*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, void> destroy;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, DiscordResult> run_callbacks;

    [NativeTypeName("void (*)(struct IDiscordCore *, enum EDiscordLogLevel, void *, void (*)(void *, enum EDiscordLogLevel, const char *))")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, DiscordLogLevel, void*, delegate* unmanaged[Cdecl]<void*, DiscordLogLevel, sbyte*, void>, void> set_log_hook;

    [NativeTypeName("struct IDiscordApplicationManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordApplicationManager*> get_application_manager;

    [NativeTypeName("struct IDiscordUserManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordUserManager*> get_user_manager;

    [NativeTypeName("struct IDiscordImageManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordImageManager*> get_image_manager;

    [NativeTypeName("struct IDiscordActivityManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordActivityManager*> get_activity_manager;

    [NativeTypeName("struct IDiscordRelationshipManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordRelationshipManager*> get_relationship_manager;

    [NativeTypeName("struct IDiscordLobbyManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordLobbyManager*> get_lobby_manager;

    [NativeTypeName("struct IDiscordNetworkManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordNetworkManager*> get_network_manager;

    [NativeTypeName("struct IDiscordOverlayManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordOverlayManager*> get_overlay_manager;

    [NativeTypeName("struct IDiscordStorageManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordStorageManager*> get_storage_manager;

    [NativeTypeName("struct IDiscordStoreManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordStoreManager*> get_store_manager;

    [NativeTypeName("struct IDiscordVoiceManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordVoiceManager*> get_voice_manager;

    [NativeTypeName("struct IDiscordAchievementManager *(*)(struct IDiscordCore *)")]
    public delegate* unmanaged[Cdecl]<IDiscordCore*, IDiscordAchievementManager*> get_achievement_manager;
}
