namespace Snap.Discord.GameSDK.ABI;

public struct DiscordMethods
{
    // void DestroyHandler(nint MethodsPtr)
    internal unsafe delegate* unmanaged[Stdcall]<void*, void> Destroy;

    // Result RunCallbacksMethod(nint methodsPtr)
    internal unsafe delegate* unmanaged[Stdcall]<void*, Result> RunCallbacks;

    // void SetLogHookCallback(nint ptr, LogLevel level, [MarshalAs(UnmanagedType.LPStr)] string message)
    // void SetLogHookMethod(nint methodsPtr, LogLevel minLevel, nint callbackData, SetLogHookCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<void*, LogLevel, void*, delegate* unmanaged[Stdcall]<nint, LogLevel, byte*, void>, void> SetLogHook;

    // nint GetApplicationManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetApplicationManager;

    // nint GetUserManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetUserManager;

    // nint GetImageManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetImageManager;

    // nint GetActivityManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetActivityManager;

    // nint GetRelationshipManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetRelationshipManager;

    // nint GetLobbyManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetLobbyManager;

    // nint GetNetworkManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetNetworkManager;

    // nint GetOverlayManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetOverlayManager;

    // nint GetStorageManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetStorageManager;

    // nint GetStorageManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetStoreManager;

    // nint GetVoiceManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetVoiceManager;

    // nint GetAchievementManagerMethod(nint discordPtr)
    internal unsafe delegate* unmanaged[Stdcall]<nint, nint> GetAchievementManager;
}