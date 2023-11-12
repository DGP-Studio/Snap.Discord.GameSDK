using Snap.Discord.GameSDK;

namespace ABI.Snap.Discord.GameSDK.Lobby;

internal partial struct LobbyMemberTransactionMethods
{
    // Result SetMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMemberTransactionMethods*, byte*, byte*, Result> SetMetadata;

    // Result DeleteMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyMemberTransactionMethods*, byte*, Result> DeleteMetadata;
}