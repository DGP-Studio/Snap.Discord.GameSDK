using Snap.Discord.GameSDK;
using Snap.Discord.GameSDK.Lobby;

namespace ABI.Snap.Discord.GameSDK.Lobby;

internal struct LobbyTransactionMethods
{
    // Result SetTypeMethod(IntPtr methodsPtr, LobbyType type)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, LobbyType, Result> SetType;

    // Result SetOwnerMethod(IntPtr methodsPtr, Int64 ownerId)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, long, Result> SetOwner;

    // Result SetCapacityMethod(IntPtr methodsPtr, UInt32 capacity)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, uint, Result> SetCapacity;

    // Result SetMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, [MarshalAs(UnmanagedType.LPStr)] string value)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, byte*, byte*, Result> SetMetadata;

    // Result DeleteMetadataMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, byte*, Result> DeleteMetadata;

    // Result SetLockedMethod(IntPtr methodsPtr, bool locked)
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, bool, Result> SetLocked;
}