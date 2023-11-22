using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public unsafe delegate TManager ManagerFactory<TMethods, TEvents, TManager>(TMethods* manager, TEvents* events)
    where TMethods : unmanaged
    where TEvents : unmanaged;

public sealed class Discord : IDisposable
{
    [DllImport("discord_game_sdk", ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    private static extern unsafe Result DiscordCreate(uint version, DiscordCreateParams* createParams, DiscordMethods** manager);

    private readonly nint selfHandle;

    private unsafe readonly DiscordAllEvents* AllEventsPtr;
    private unsafe readonly DiscordMethods* MethodsPtr;

    private bool disposed;

    [Obsolete("Deprecated by Discord")]
    internal ApplicationManager? ApplicationManagerInstance;
    internal UserManager? UserManagerInstance;

    [Obsolete("Deprecated by Discord")]
    internal ImageManager? ImageManagerInstance;
    internal ActivityManager? ActivityManagerInstance;
    internal RelationshipManager? RelationshipManagerInstance;

    [Obsolete("Deprecated by Discord")]
    internal LobbyManager? LobbyManagerInstance;

    [Obsolete("Deprecated by Discord")]
    internal NetworkManager? NetworkManagerInstance;
    internal OverlayManager? OverlayManagerInstance;

    [Obsolete("Deprecated by Discord")]
    internal StorageManager? StorageManagerInstance;

    [Obsolete("Deprecated by Discord")]
    internal StoreManager? StoreManagerInstance;

    [Obsolete("Deprecated by Discord")]
    internal VoiceManager? VoiceManagerInstance;

    [Obsolete("Deprecated by Discord")]
    internal AchievementManager? AchievementManagerInstance;

    /// <summary>
    /// Creates an instance of Discord to initialize the SDK. This is the overlord of all things Discord. We like to call her Nelly.
    /// </summary>
    /// <param name="clientId">your application's client id</param>
    /// <param name="flags">the creation parameters for the SDK</param>
    public unsafe Discord(long clientId, CreateFlags flags)
    {
        ClientId = clientId;
        AllEventsPtr = UnsafeNativeMemory.Alloc<DiscordAllEvents>();
        selfHandle = DiscordGCHandle.Alloc(this);

        DiscordCreateParams createParams = DiscordCreateParams.Create(clientId, flags, AllEventsPtr, selfHandle);

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

    ~Discord()
    {
        Dispose(false);
    }

    public bool Disposed { get => disposed; }

    public long ClientId { get; private set; }

    /// <summary>
    /// Destroys the instance. Wave goodbye, Nelly! You monster.
    /// </summary>
    public unsafe void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
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
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void SetLogHookCallbackImpl(SetLogHookHandler ptr, LogLevel level, byte* message)
        {
            ptr.Invoke(level, message);
        }

        MethodsPtr->SetLogHook.Invoke(MethodsPtr, minLevel, callback, SetLogHookCallback.Create(&SetLogHookCallbackImpl));
    }

    [Obsolete("Deprecated by Discord")]
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

    /// <inheritdoc cref="GetUserManager()"/>
    public unsafe UserManager GetUserManager<TUserManager>(ManagerFactory<UserMethods, UserEvents, TUserManager> factory)
        where TUserManager : UserManager
    {
        return UserManagerInstance ??= factory(MethodsPtr->GetUserManager.Invoke(MethodsPtr), &AllEventsPtr->UserEvents);
    }

    [Obsolete("Deprecated by Discord")]
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

    /// <inheritdoc cref="GetActivityManager()"/>
    public unsafe ActivityManager GetActivityManager<TActivityManager>(ManagerFactory<ActivityMethods, ActivityEvents, TActivityManager> factory)
        where TActivityManager : ActivityManager
    {
        return ActivityManagerInstance ??= factory(MethodsPtr->GetActivityManager.Invoke(MethodsPtr), &AllEventsPtr->ActivityEvents);
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

    /// <inheritdoc cref="GetRelationshipManager()"/>
    public unsafe RelationshipManager GetRelationshipManager<TRelationshipManager>(ManagerFactory<RelationshipMethods, RelationshipEvents, TRelationshipManager> factory)
        where TRelationshipManager : RelationshipManager
    {
        return RelationshipManagerInstance ??= factory(MethodsPtr->GetRelationshipManager.Invoke(MethodsPtr), &AllEventsPtr->RelationshipEvents);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe LobbyManager GetLobbyManager()
    {
        return LobbyManagerInstance ??= new LobbyManager(MethodsPtr->GetLobbyManager.Invoke(MethodsPtr), &AllEventsPtr->LobbyEvents);
    }

    [Obsolete("Deprecated by Discord")]
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

    /// <inheritdoc cref="GetOverlayManager()"/>
    public unsafe OverlayManager GetOverlayManager<TOverlayManager>(ManagerFactory<OverlayMethods, OverlayEvents, TOverlayManager> factory)
        where TOverlayManager : OverlayManager
    {
        return OverlayManagerInstance ??= factory(MethodsPtr->GetOverlayManager.Invoke(MethodsPtr), &AllEventsPtr->OverlayEvents);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe StorageManager GetStorageManager()
    {
        return StorageManagerInstance ??= new StorageManager(MethodsPtr->GetStorageManager.Invoke(MethodsPtr), &AllEventsPtr->StorageEvents);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe StoreManager GetStoreManager()
    {
        return StoreManagerInstance ??= new StoreManager(MethodsPtr->GetStoreManager.Invoke(MethodsPtr), &AllEventsPtr->StoreEvents);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe VoiceManager GetVoiceManager()
    {
        return VoiceManagerInstance ??= new VoiceManager(MethodsPtr->GetVoiceManager.Invoke(MethodsPtr), &AllEventsPtr->VoiceEvents);
    }

    [Obsolete("Deprecated by Discord")]
    public unsafe AchievementManager GetAchievementManager()
    {
        return AchievementManagerInstance ??= new AchievementManager(MethodsPtr->GetAchievementManager.Invoke(MethodsPtr), &AllEventsPtr->AchievementEvents);
    }

    private unsafe void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // Release managed resources, which is null for us
            }

            // Seems like sdk handles the memory free
            // we don't want to call NativeMemory.Free(MethodsPtr) here
            if (MethodsPtr is not null)
            {
                MethodsPtr->Destroy.Invoke(MethodsPtr);
            }

            DiscordGCHandle.Free(selfHandle);
            NativeMemory.Free(AllEventsPtr);

            disposed = true;
        }
    }
}