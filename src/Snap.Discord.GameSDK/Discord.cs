using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public sealed class Discord : IDisposable
{
    [DllImport("discord_game_sdk", ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern unsafe Result DiscordCreate(uint version, DiscordCreateParams* createParams, /* out */ DiscordMethods** manager);

    private readonly nint SelfHandle;

    private unsafe readonly DiscordAllEvents* AllEventsPtr;
    private unsafe readonly DiscordMethods* MethodsPtr;

    internal ApplicationManager? ApplicationManagerInstance;
    internal UserManager? UserManagerInstance;
    internal ImageManager? ImageManagerInstance;
    internal ActivityManager? ActivityManagerInstance;
    internal RelationshipManager? RelationshipManagerInstance;
    internal LobbyManager? LobbyManagerInstance;
    internal NetworkManager? NetworkManagerInstance;
    internal OverlayManager? OverlayManagerInstance;
    internal StorageManager? StorageManagerInstance;
    internal StoreManager? StoreManagerInstance;
    internal VoiceManager? VoiceManagerInstance;
    internal AchievementManager? AchievementManagerInstance;

    /// <summary>
    /// Creates an instance of Discord to initialize the SDK. This is the overlord of all things Discord. We like to call her Nelly.
    /// </summary>
    /// <param name="clientId">your application's client id</param>
    /// <param name="flags">the creation parameters for the SDK</param>
    public unsafe Discord(long clientId, CreateFlags flags)
    {
        DiscordCreateParams createParams = default;
        createParams.ClientId = clientId;
        createParams.Flags = flags;

        AllEventsPtr = UnsafeNativeMemory.Alloc<DiscordAllEvents>();
        createParams.Events = &AllEventsPtr->DiscordEvents;

        SelfHandle = DiscordGCHandle.Alloc(this);
        createParams.EventData = SelfHandle;

        createParams.ApplicationEvents = &AllEventsPtr->ApplicationEvents;
        createParams.ApplicationVersion = 1;

        createParams.UserEvents = &AllEventsPtr->UserEvents;
        createParams.UserVersion = 1;

        createParams.ImageEvents = &AllEventsPtr->ImageEvents;
        createParams.ImageVersion = 1;

        createParams.ActivityEvents = &AllEventsPtr->ActivityEvents;
        createParams.ActivityVersion = 1;

        createParams.RelationshipEvents = &AllEventsPtr->RelationshipEvents;
        createParams.RelationshipVersion = 1;

        createParams.LobbyEvents = &AllEventsPtr->LobbyEvents;
        createParams.LobbyVersion = 1;

        createParams.NetworkEvents = &AllEventsPtr->NetworkEvents;
        createParams.NetworkVersion = 1;

        createParams.OverlayEvents = &AllEventsPtr->OverlayEvents;
        createParams.OverlayVersion = 2;

        createParams.StorageEvents = &AllEventsPtr->StorageEvents;
        createParams.StorageVersion = 1;

        createParams.StoreEvents = &AllEventsPtr->StoreEvents;
        createParams.StoreVersion = 1;

        createParams.VoiceEvents = &AllEventsPtr->VoiceEvents;
        createParams.VoiceVersion = 1;

        createParams.AchievementEvents = &AllEventsPtr->AchievementEvents;
        createParams.AchievementVersion = 1;

        try
        {
            MethodsPtr = UnsafeNativeMemory.Alloc<DiscordMethods>();
            fixed (DiscordMethods** ppv = &MethodsPtr)
            {
                DiscordCreate(3, &createParams, ppv).ThrowOnFailure();
            }
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    /// <summary>
    /// Destroys the instance. Wave goodbye, Nelly! You monster.
    /// </summary>
    public unsafe void Dispose()
    {
        if (MethodsPtr is not null)
        {
            MethodsPtr->Destroy.Invoke(MethodsPtr);
        }

        DiscordGCHandle.Free(SelfHandle);
        NativeMemory.Free(AllEventsPtr);
        NativeMemory.Free(MethodsPtr);
    }

    /// <summary>
    /// Runs all pending SDK callbacks. Put this in your game's main event loop, like Update() in Unity. That way, the first thing your game does is check for any new info from Discord.
    /// <para/>
    /// This function also serves as a way to know that the local Discord client is still connected. If the user closes Discord while playing your game, RunCallbacks() will return/throw <see cref="Result.NotRunning"/>.
    /// </summary>
    public unsafe void RunCallbacks()
    {
        MethodsPtr->RunCallbacks.Invoke(MethodsPtr).ThrowOnFailure();
    }

    /// <summary>
    /// Registers a logging callback function with the minimum level of message to receive. 
    /// </summary>
    /// <param name="minLevel">the minimum level of event to log</param>
    /// <param name="callback">the callback function to catch the messages</param>
    public unsafe void SetLogHook(LogLevel minLevel, SetLogHookHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void SetLogHookCallbackImpl(SetLogHookHandler ptr, LogLevel level, byte* message)
        {
            ptr.Invoke(level, message);
        }

        MethodsPtr->SetLogHook.Invoke(MethodsPtr, minLevel, callback, SetLogHookCallback.Create(&SetLogHookCallbackImpl));
    }

    [Obsolete]
    public unsafe ApplicationManager GetApplicationManager()
    {
        return ApplicationManagerInstance ??= new ApplicationManager(MethodsPtr->GetApplicationManager.Invoke(MethodsPtr), &AllEventsPtr->ApplicationEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with users in the SDK.
    /// for fetching user data for a given id and the current user
    /// </summary>
    /// <returns></returns>
    public unsafe UserManager GetUserManager()
    {
        return UserManagerInstance ??= new UserManager(MethodsPtr->GetUserManager.Invoke(MethodsPtr), &AllEventsPtr->UserEvents);
    }

    [Obsolete]
    public unsafe ImageManager GetImageManager()
    {
        return ImageManagerInstance ??= new ImageManager(MethodsPtr->GetImageManager.Invoke(MethodsPtr), &AllEventsPtr->ImageEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with activities in the SDK.
    /// for Rich Presence and game invites
    /// </summary>
    /// <returns></returns>
    public unsafe ActivityManager GetActivityManager()
    {
        return ActivityManagerInstance ??= new ActivityManager(MethodsPtr->GetActivityManager.Invoke(MethodsPtr), &AllEventsPtr->ActivityEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with relationships in the SDK.
    /// for users' social relationships across Discord, including friends list
    /// </summary>
    /// <returns></returns>
    public unsafe RelationshipManager GetRelationshipManager()
    {
        return RelationshipManagerInstance ??= new RelationshipManager(MethodsPtr->GetRelationshipManager.Invoke(MethodsPtr), &AllEventsPtr->RelationshipEvents);
    }

    [Obsolete]
    public unsafe LobbyManager GetLobbyManager()
    {
        return LobbyManagerInstance ??= new LobbyManager(MethodsPtr->GetLobbyManager.Invoke(MethodsPtr), &AllEventsPtr->LobbyEvents);
    }

    [Obsolete]
    public unsafe NetworkManager GetNetworkManager()
    {
        return NetworkManagerInstance ??= new NetworkManager(MethodsPtr->GetNetworkManager.Invoke(MethodsPtr), &AllEventsPtr->NetworkEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with the overlay in the SDK.
    /// for interacting with Discord's built-in overlay
    /// </summary>
    /// <returns></returns>
    public unsafe OverlayManager GetOverlayManager()
    {
        return OverlayManagerInstance ??= new OverlayManager(MethodsPtr->GetOverlayManager.Invoke(MethodsPtr), &AllEventsPtr->OverlayEvents);
    }

    [Obsolete]
    public unsafe StorageManager GetStorageManager()
    {
        return StorageManagerInstance ??= new StorageManager(MethodsPtr->GetStorageManager.Invoke(MethodsPtr), &AllEventsPtr->StorageEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with SKUs and Entitlements in the SDK.
    /// for all things entitlements and SKUs, including IAP
    /// </summary>
    /// <returns></returns>
    public unsafe StoreManager GetStoreManager()
    {
        return StoreManagerInstance ??= new StoreManager(MethodsPtr->GetStoreManager.Invoke(MethodsPtr), &AllEventsPtr->StoreEvents);
    }

    [Obsolete]
    public unsafe VoiceManager GetVoiceManager()
    {
        return VoiceManagerInstance ??= new VoiceManager(MethodsPtr->GetVoiceManager.Invoke(MethodsPtr), &AllEventsPtr->VoiceEvents);
    }

    [Obsolete]
    public unsafe AchievementManager GetAchievementManager()
    {
        return AchievementManagerInstance ??= new AchievementManager(MethodsPtr->GetAchievementManager.Invoke(MethodsPtr), &AllEventsPtr->AchievementEvents);
    }
}