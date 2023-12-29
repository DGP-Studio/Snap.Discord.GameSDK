namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordStoreManager
{
    [NativeTypeName("void (*)(struct IDiscordStoreManager *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> fetch_skus;

    [NativeTypeName("void (*)(struct IDiscordStoreManager *, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, int*, void> count_skus;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStoreManager *, DiscordSnowflake, struct DiscordSku *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, long, DiscordSku*, DiscordResult> get_sku;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStoreManager *, int32_t, struct DiscordSku *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, int, DiscordSku*, DiscordResult> get_sku_at;

    [NativeTypeName("void (*)(struct IDiscordStoreManager *, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> fetch_entitlements;

    [NativeTypeName("void (*)(struct IDiscordStoreManager *, int32_t *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, int*, void> count_entitlements;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStoreManager *, DiscordSnowflake, struct DiscordEntitlement *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, long, DiscordEntitlement*, DiscordResult> get_entitlement;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStoreManager *, int32_t, struct DiscordEntitlement *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, int, DiscordEntitlement*, DiscordResult> get_entitlement_at;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordStoreManager *, DiscordSnowflake, bool *)")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, long, bool*, DiscordResult> has_sku_entitlement;

    [NativeTypeName("void (*)(struct IDiscordStoreManager *, DiscordSnowflake, void *, void (*)(void *, enum EDiscordResult))")]
    public delegate* unmanaged[Cdecl]<IDiscordStoreManager*, long, void*, delegate* unmanaged[Cdecl]<void*, DiscordResult, void>, void> start_purchase;
}
