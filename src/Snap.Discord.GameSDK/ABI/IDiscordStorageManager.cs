namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordStorageManager
{
    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStorageManager *, const char *, uint8_t *, uint32_t, uint32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, byte*, uint, uint*, DiscordResult> read;

    [NativeTypeName("void (*)(struct IDiscordStorageManager *, const char *, void *, void (*)(void *, enum EDiscordResult, uint8_t *, uint32_t))")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, byte*, uint, void>, void> read_async;

    [NativeTypeName("void (*)(struct IDiscordStorageManager *, const char *, uint64_t, uint64_t, void *, void (*)(void *, enum EDiscordResult, uint8_t *, uint32_t))")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, ulong, ulong, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, byte*, uint, void>, void> read_async_partial;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStorageManager *, const char *, uint8_t *, uint32_t)")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, byte*, uint, DiscordResult> write;

    [NativeTypeName("void (*)(struct IDiscordStorageManager *, const char *, uint8_t *, uint32_t, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, byte*, uint, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> write_async;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStorageManager *, const char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, DiscordResult> delete_;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStorageManager *, const char *, bool *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, bool*, DiscordResult> exists;

    [NativeTypeName("void (*)(struct IDiscordStorageManager *, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, int*, void> count;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStorageManager *, const char *, struct DiscordFileStat *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, DiscordFileStat*, DiscordResult> stat;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStorageManager *, int32_t, struct DiscordFileStat *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, int, DiscordFileStat*, DiscordResult> stat_at;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStorageManager *, DiscordPath *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStorageManager*, sbyte*, DiscordResult> get_path;
}
