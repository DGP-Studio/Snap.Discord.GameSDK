namespace Snap.Discord.GameSDK.ABI;

public struct LobbyTransactionMethods
{
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, LobbyType, Result> SetType;
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, long, Result> SetOwner;
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, uint, Result> SetCapacity;
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, byte*, byte*, Result> SetMetadata;
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, byte*, Result> DeleteMetadata;
    internal unsafe delegate* unmanaged[Stdcall]<LobbyTransactionMethods*, bool, Result> SetLocked;
}