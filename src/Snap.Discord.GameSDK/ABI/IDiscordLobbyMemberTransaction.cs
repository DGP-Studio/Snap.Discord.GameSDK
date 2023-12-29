namespace Snap.Discord.GameSDK.ABI;

public unsafe partial struct IDiscordLobbyMemberTransaction
{
    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyMemberTransaction *, char *, char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyMemberTransaction*, sbyte, sbyte, DiscordResult> set_metadata;

    [NativeTypeName("enum EDiscordResult (*)(struct IDiscordLobbyMemberTransaction *, char *)")]
    public delegate* unmanaged[Cdecl]<IDiscordLobbyMemberTransaction*, sbyte, DiscordResult> delete_metadata;
}
