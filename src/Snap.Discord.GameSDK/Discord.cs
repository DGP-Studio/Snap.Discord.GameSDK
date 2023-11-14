using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public sealed class Discord : IDisposable
{
    [DllImport("discord_game_sdk", ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    private static extern unsafe Result DiscordCreate(uint version, DiscordCreateParams* createParams, /* out */ DiscordMethods* manager);

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

    private unsafe readonly DiscordMethods* MethodsPtr;

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
        try
        {
            DiscordCreate(3, &createParams, MethodsPtr).ThrowOnFailure();
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

    public unsafe void RunCallbacks()
    {
        MethodsPtr->RunCallbacks.Invoke(MethodsPtr).ThrowOnFailure();
    }

    public unsafe void SetLogHook(LogLevel minLevel, SetLogHookHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void SetLogHookCallbackImpl(SetLogHookHandler ptr, LogLevel level, byte* message)
        {
            ptr.Invoke(level, message);
        }

        MethodsPtr->SetLogHook.Invoke(MethodsPtr, minLevel, callback, SetLogHookCallback.Create(&SetLogHookCallbackImpl));
    }

    public unsafe ApplicationManager GetApplicationManager()
    {
        return ApplicationManagerInstance ??= new ApplicationManager(MethodsPtr->GetApplicationManager.Invoke(MethodsPtr), ApplicationEventsPtr, ref ApplicationEvents);
    }

    public unsafe UserManager GetUserManager()
    {
        return UserManagerInstance ??= new UserManager(MethodsPtr->GetUserManager.Invoke(MethodsPtr), UserEventsPtr, ref UserEvents);
    }

    public unsafe ImageManager GetImageManager()
    {
        return ImageManagerInstance ??= new ImageManager(MethodsPtr->GetImageManager.Invoke(MethodsPtr), ImageEventsPtr, ref ImageEvents);
    }

    public unsafe ActivityManager GetActivityManager()
    {
        return ActivityManagerInstance ??= new ActivityManager(MethodsPtr->GetActivityManager.Invoke(MethodsPtr), ActivityEventsPtr, ref ActivityEvents);
    }

    public unsafe RelationshipManager GetRelationshipManager()
    {
        return RelationshipManagerInstance ??= new RelationshipManager(MethodsPtr->GetRelationshipManager.Invoke(MethodsPtr), RelationshipEventsPtr, ref RelationshipEvents);
    }

    public unsafe LobbyManager GetLobbyManager()
    {
        return LobbyManagerInstance ??= new LobbyManager(MethodsPtr->GetLobbyManager.Invoke(MethodsPtr), LobbyEventsPtr, ref LobbyEvents);
    }

    public unsafe NetworkManager GetNetworkManager()
    {
        return NetworkManagerInstance ??= new NetworkManager(MethodsPtr->GetNetworkManager.Invoke(MethodsPtr), NetworkEventsPtr, ref NetworkEvents);
    }

    public unsafe OverlayManager GetOverlayManager()
    {
        return OverlayManagerInstance ??= new OverlayManager(MethodsPtr->GetOverlayManager.Invoke(MethodsPtr), OverlayEventsPtr, ref OverlayEvents);
    }

    public unsafe StorageManager GetStorageManager()
    {
        return StorageManagerInstance ??= new StorageManager(MethodsPtr->GetStorageManager.Invoke(MethodsPtr), StorageEventsPtr, ref StorageEvents);
    }

    public unsafe StoreManager GetStoreManager()
    {
        return StoreManagerInstance ??= new StoreManager(MethodsPtr->GetStoreManager.Invoke(MethodsPtr), StoreEventsPtr, ref StoreEvents);
    }

    public unsafe VoiceManager GetVoiceManager()
    {
        return VoiceManagerInstance ??= new VoiceManager(MethodsPtr->GetVoiceManager.Invoke(MethodsPtr), VoiceEventsPtr, ref VoiceEvents);
    }

    public unsafe AchievementManager GetAchievementManager()
    {
        return AchievementManagerInstance ??= new AchievementManager(MethodsPtr->GetAchievementManager.Invoke(MethodsPtr), AchievementEventsPtr, ref AchievementEvents);
    }
}