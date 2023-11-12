using Snap.Discord.GameSDK;
using Snap.Discord.GameSDK.Lobby;

namespace ABI.Snap.Discord.GameSDK.Lobby;

internal struct LobbySearchQueryMethods
{
    // Result FilterMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, LobbySearchComparison comparison, LobbySearchCast cast, [MarshalAs(UnmanagedType.LPStr)] string value)
    internal unsafe delegate* unmanaged[Stdcall]<LobbySearchQueryMethods*, byte*, LobbySearchComparison, LobbySearchCast, byte*, Result> Filter;

    // Result SortMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string key, LobbySearchCast cast, [MarshalAs(UnmanagedType.LPStr)] string value)
    internal unsafe delegate* unmanaged[Stdcall]<LobbySearchQueryMethods*, byte*, LobbySearchCast, byte*, Result> Sort;

    // Result LimitMethod(IntPtr methodsPtr, UInt32 limit)
    internal unsafe delegate* unmanaged[Stdcall]<LobbySearchQueryMethods*, uint, Result> Limit;

    // Result DistanceMethod(IntPtr methodsPtr, LobbySearchDistance distance)
    internal unsafe delegate* unmanaged[Stdcall]<LobbySearchQueryMethods*, LobbySearchDistance, Result> Distance;
}