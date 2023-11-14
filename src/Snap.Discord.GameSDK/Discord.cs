using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class Discord : IDisposable
{
    [DllImport("discord_game_sdk", ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern unsafe Result DiscordCreate(uint version, DiscordCreateParams* createParams, /* out */ DiscordMethods* manager);

    private nint SelfHandle;

    private unsafe DiscordEvents* EventsPtr;

    private DiscordEvents Events;

    private unsafe ApplicationEvents* ApplicationEventsPtr;

    private ApplicationEvents ApplicationEvents;

    internal ApplicationManager? ApplicationManagerInstance;

    private unsafe UserEvents* UserEventsPtr;

    private UserEvents UserEvents;

    internal UserManager? UserManagerInstance;

    private unsafe ImageEvents* ImageEventsPtr;

    private ImageEvents ImageEvents;

    internal ImageManager? ImageManagerInstance;

    private unsafe ActivityEvents* ActivityEventsPtr;

    private ActivityEvents ActivityEvents;

    internal ActivityManager? ActivityManagerInstance;

    private unsafe RelationshipEvents* RelationshipEventsPtr;

    private RelationshipEvents RelationshipEvents;

    internal RelationshipManager? RelationshipManagerInstance;

    private unsafe LobbyEvents* LobbyEventsPtr;

    private LobbyEvents LobbyEvents;

    internal LobbyManager? LobbyManagerInstance;

    private unsafe NetworkEvents* NetworkEventsPtr;

    private NetworkEvents NetworkEvents;

    internal NetworkManager? NetworkManagerInstance;

    private unsafe OverlayEvents* OverlayEventsPtr;

    private OverlayEvents OverlayEvents;

    internal OverlayManager? OverlayManagerInstance;

    private unsafe StorageEvents* StorageEventsPtr;

    private StorageEvents StorageEvents;

    internal StorageManager? StorageManagerInstance;

    private unsafe StoreEvents* StoreEventsPtr;

    private StoreEvents StoreEvents;

    internal StoreManager? StoreManagerInstance;

    private unsafe VoiceEvents* VoiceEventsPtr;

    private VoiceEvents VoiceEvents;

    internal VoiceManager? VoiceManagerInstance;

    private unsafe AchievementEvents* AchievementEventsPtr;

    private AchievementEvents AchievementEvents;

    internal AchievementManager? AchievementManagerInstance;

    private unsafe DiscordMethods* MethodsPtr;

    private nint padding;

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

        Events = default;
        EventsPtr = UnsafeNativeMemory.Alloc<DiscordEvents>();
        createParams.Events = EventsPtr;

        SelfHandle = DiscordGCHandle.Alloc(this);
        createParams.EventData = SelfHandle;

        ApplicationEvents = default;
        ApplicationEventsPtr = UnsafeNativeMemory.Alloc<ApplicationEvents>();
        createParams.ApplicationEvents = ApplicationEventsPtr;
        createParams.ApplicationVersion = 1;

        UserEvents = default;
        UserEventsPtr = UnsafeNativeMemory.Alloc<UserEvents>();
        createParams.UserEvents = UserEventsPtr;
        createParams.UserVersion = 1;

        ImageEvents = default;
        ImageEventsPtr = UnsafeNativeMemory.Alloc<ImageEvents>();
        createParams.ImageEvents = ImageEventsPtr;
        createParams.ImageVersion = 1;

        ActivityEvents = default;
        ActivityEventsPtr = UnsafeNativeMemory.Alloc<ActivityEvents>();
        createParams.ActivityEvents = ActivityEventsPtr;
        createParams.ActivityVersion = 1;

        RelationshipEvents = default;
        RelationshipEventsPtr = UnsafeNativeMemory.Alloc<RelationshipEvents>();
        createParams.RelationshipEvents = RelationshipEventsPtr;
        createParams.RelationshipVersion = 1;

        LobbyEvents = default;
        LobbyEventsPtr = UnsafeNativeMemory.Alloc<LobbyEvents>();
        createParams.LobbyEvents = LobbyEventsPtr;
        createParams.LobbyVersion = 1;

        NetworkEvents = default;
        NetworkEventsPtr = UnsafeNativeMemory.Alloc<NetworkEvents>();
        createParams.NetworkEvents = NetworkEventsPtr;
        createParams.NetworkVersion = 1;

        OverlayEvents = default;
        OverlayEventsPtr = UnsafeNativeMemory.Alloc<OverlayEvents>();
        createParams.OverlayEvents = OverlayEventsPtr;
        createParams.OverlayVersion = 2;

        StorageEvents = default;
        StorageEventsPtr = UnsafeNativeMemory.Alloc<StorageEvents>();
        createParams.StorageEvents = StorageEventsPtr;
        createParams.StorageVersion = 1;

        StoreEvents = default;
        StoreEventsPtr = UnsafeNativeMemory.Alloc<StoreEvents>();
        createParams.StoreEvents = StoreEventsPtr;
        createParams.StoreVersion = 1;

        VoiceEvents = default;
        VoiceEventsPtr = UnsafeNativeMemory.Alloc<VoiceEvents>();
        createParams.VoiceEvents = VoiceEventsPtr;
        createParams.VoiceVersion = 1;

        AchievementEvents = default;
        AchievementEventsPtr = UnsafeNativeMemory.Alloc<AchievementEvents>();
        createParams.AchievementEvents = AchievementEventsPtr;
        createParams.AchievementVersion = 1;

        InitEvents(EventsPtr, ref Events);
        DiscordMethods* methodsPtr = default;
        try
        {
            DiscordCreate(3, &createParams, methodsPtr).ThrowOnFailure();
            //DiscordCreate(3, &createParams, methodsPtr).ThrowOnFailure();
            MethodsPtr = methodsPtr;
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    private static unsafe void InitEvents(DiscordEvents* eventsPtr, ref DiscordEvents events)
    {
        *eventsPtr = events;
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
        NativeMemory.Free(EventsPtr);
        NativeMemory.Free(EventsPtr);
        NativeMemory.Free(ApplicationEventsPtr);
        NativeMemory.Free(UserEventsPtr);
        NativeMemory.Free(ImageEventsPtr);
        NativeMemory.Free(ActivityEventsPtr);
        NativeMemory.Free(RelationshipEventsPtr);
        NativeMemory.Free(LobbyEventsPtr);
        NativeMemory.Free(NetworkEventsPtr);
        NativeMemory.Free(OverlayEventsPtr);
        NativeMemory.Free(StorageEventsPtr);
        NativeMemory.Free(StoreEventsPtr);
        NativeMemory.Free(VoiceEventsPtr);
        NativeMemory.Free(AchievementEventsPtr);
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
        return ApplicationManagerInstance ??= new ApplicationManager(MethodsPtr->GetApplicationManager.Invoke(MethodsPtr), ApplicationEventsPtr, ref ApplicationEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with users in the SDK.
    /// for fetching user data for a given id and the current user
    /// </summary>
    /// <returns></returns>
    public unsafe UserManager GetUserManager()
    {
        return UserManagerInstance ??= new UserManager(MethodsPtr->GetUserManager.Invoke(MethodsPtr), UserEventsPtr, ref UserEvents);
    }

    [Obsolete]
    public unsafe ImageManager GetImageManager()
    {
        return ImageManagerInstance ??= new ImageManager(MethodsPtr->GetImageManager.Invoke(MethodsPtr), ImageEventsPtr, ref ImageEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with activities in the SDK.
    /// for Rich Presence and game invites
    /// </summary>
    /// <returns></returns>
    public unsafe ActivityManager GetActivityManager()
    {
        return ActivityManagerInstance ??= new ActivityManager(MethodsPtr->GetActivityManager.Invoke(MethodsPtr), ActivityEventsPtr, ref ActivityEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with relationships in the SDK.
    /// for users' social relationships across Discord, including friends list
    /// </summary>
    /// <returns></returns>
    public unsafe RelationshipManager GetRelationshipManager()
    {
        return RelationshipManagerInstance ??= new RelationshipManager(MethodsPtr->GetRelationshipManager.Invoke(MethodsPtr), RelationshipEventsPtr, ref RelationshipEvents);
    }

    [Obsolete]
    public unsafe LobbyManager GetLobbyManager()
    {
        return LobbyManagerInstance ??= new LobbyManager(MethodsPtr->GetLobbyManager.Invoke(MethodsPtr), LobbyEventsPtr, ref LobbyEvents);
    }

    [Obsolete]
    public unsafe NetworkManager GetNetworkManager()
    {
        return NetworkManagerInstance ??= new NetworkManager(MethodsPtr->GetNetworkManager.Invoke(MethodsPtr), NetworkEventsPtr, ref NetworkEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with the overlay in the SDK.
    /// for interacting with Discord's built-in overlay
    /// </summary>
    /// <returns></returns>
    public unsafe OverlayManager GetOverlayManager()
    {
        return OverlayManagerInstance ??= new OverlayManager(MethodsPtr->GetOverlayManager.Invoke(MethodsPtr), OverlayEventsPtr, ref OverlayEvents);
    }

    [Obsolete]
    public unsafe StorageManager GetStorageManager()
    {
        return StorageManagerInstance ??= new StorageManager(MethodsPtr->GetStorageManager.Invoke(MethodsPtr), StorageEventsPtr, ref StorageEvents);
    }

    /// <summary>
    /// Fetches an instance of the manager for interfacing with SKUs and Entitlements in the SDK.
    /// for all things entitlements and SKUs, including IAP
    /// </summary>
    /// <returns></returns>
    public unsafe StoreManager GetStoreManager()
    {
        return StoreManagerInstance ??= new StoreManager(MethodsPtr->GetStoreManager.Invoke(MethodsPtr), StoreEventsPtr, ref StoreEvents);
    }

    [Obsolete]
    public unsafe VoiceManager GetVoiceManager()
    {
        return VoiceManagerInstance ??= new VoiceManager(MethodsPtr->GetVoiceManager.Invoke(MethodsPtr), VoiceEventsPtr, ref VoiceEvents);
    }

    [Obsolete]
    public unsafe AchievementManager GetAchievementManager()
    {
        return AchievementManagerInstance ??= new AchievementManager(MethodsPtr->GetAchievementManager.Invoke(MethodsPtr), AchievementEventsPtr, ref AchievementEvents);
    }
}