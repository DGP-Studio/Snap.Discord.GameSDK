using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class Discord : IDisposable
{
    [DllImport(Constants.DllName, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    private static extern unsafe Result DiscordCreate(uint version, DiscordCreateParams* createParams, /* out */ DiscordMethods* manager);

    public delegate void SetLogHookHandler(LogLevel level, string message);

    private nint SelfHandle;

    private unsafe DiscordEvents* EventsPtr;

    private DiscordEvents Events;

    private unsafe ApplicationEvents* ApplicationEventsPtr;

    private ApplicationEvents ApplicationEvents;

    internal ApplicationManager ApplicationManagerInstance;

    private nint UserEventsPtr;

    private UserManager.FFIEvents UserEvents;

    internal UserManager UserManagerInstance;

    private nint ImageEventsPtr;

    private ImageManager.FFIEvents ImageEvents;

    internal ImageManager ImageManagerInstance;

    private nint ActivityEventsPtr;

    private ActivityManager.FFIEvents ActivityEvents;

    internal ActivityManager ActivityManagerInstance;

    private nint RelationshipEventsPtr;

    private RelationshipManager.FFIEvents RelationshipEvents;

    internal RelationshipManager RelationshipManagerInstance;

    private nint LobbyEventsPtr;

    private LobbyManager.FFIEvents LobbyEvents;

    internal LobbyManager LobbyManagerInstance;

    private nint NetworkEventsPtr;

    private NetworkManager.FFIEvents NetworkEvents;

    internal NetworkManager NetworkManagerInstance;

    private nint OverlayEventsPtr;

    private OverlayManager.FFIEvents OverlayEvents;

    internal OverlayManager OverlayManagerInstance;

    private nint StorageEventsPtr;

    private StorageManager.FFIEvents StorageEvents;

    internal StorageManager StorageManagerInstance;

    private nint StoreEventsPtr;

    private StoreManager.FFIEvents StoreEvents;

    internal StoreManager StoreManagerInstance;

    private nint VoiceEventsPtr;

    private VoiceManager.FFIEvents VoiceEvents;

    internal VoiceManager VoiceManagerInstance;

    private nint AchievementEventsPtr;

    private AchievementManager.FFIEvents AchievementEvents;

    internal AchievementManager AchievementManagerInstance;

    private unsafe DiscordMethods* MethodsPtr;

    private GCHandle? setLogHook;

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
        UserEvents = new UserManager.FFIEvents();
        UserEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(UserEvents));
        createParams.UserEvents = UserEventsPtr;
        createParams.UserVersion = 1;
        ImageEvents = new ImageManager.FFIEvents();
        ImageEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ImageEvents));
        createParams.ImageEvents = ImageEventsPtr;
        createParams.ImageVersion = 1;
        ActivityEvents = new ActivityManager.FFIEvents();
        ActivityEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ActivityEvents));
        createParams.ActivityEvents = ActivityEventsPtr;
        createParams.ActivityVersion = 1;
        RelationshipEvents = new RelationshipManager.FFIEvents();
        RelationshipEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(RelationshipEvents));
        createParams.RelationshipEvents = RelationshipEventsPtr;
        createParams.RelationshipVersion = 1;
        LobbyEvents = new LobbyManager.FFIEvents();
        LobbyEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(LobbyEvents));
        createParams.LobbyEvents = LobbyEventsPtr;
        createParams.LobbyVersion = 1;
        NetworkEvents = new NetworkManager.FFIEvents();
        NetworkEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(NetworkEvents));
        createParams.NetworkEvents = NetworkEventsPtr;
        createParams.NetworkVersion = 1;
        OverlayEvents = new OverlayManager.FFIEvents();
        OverlayEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(OverlayEvents));
        createParams.OverlayEvents = OverlayEventsPtr;
        createParams.OverlayVersion = 2;
        StorageEvents = new StorageManager.FFIEvents();
        StorageEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(StorageEvents));
        createParams.StorageEvents = StorageEventsPtr;
        createParams.StorageVersion = 1;
        StoreEvents = new StoreManager.FFIEvents();
        StoreEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(StoreEvents));
        createParams.StoreEvents = StoreEventsPtr;
        createParams.StoreVersion = 1;
        VoiceEvents = new VoiceManager.FFIEvents();
        VoiceEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(VoiceEvents));
        createParams.VoiceEvents = VoiceEventsPtr;
        createParams.VoiceVersion = 1;
        AchievementEvents = new AchievementManager.FFIEvents();
        AchievementEventsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(AchievementEvents));
        createParams.AchievementEvents = AchievementEventsPtr;
        createParams.AchievementVersion = 1;
        InitEvents(EventsPtr, ref Events);
        Result result = DiscordCreate(3, &createParams, out MethodsPtr);
        if (result is not Result.Ok)
        {
            Dispose();
            throw new ResultException(result);
        }
    }

    private void InitEvents(nint eventsPtr, ref DiscordEvents events)
    {
        Marshal.StructureToPtr(events, eventsPtr, false);
    }

    public void Dispose()
    {
        if (MethodsPtr != nint.Zero)
        {
            Methods.Destroy(MethodsPtr);
        }
        SelfHandle.Free();
        Marshal.FreeHGlobal(EventsPtr);
        Marshal.FreeHGlobal(ApplicationEventsPtr);
        Marshal.FreeHGlobal(UserEventsPtr);
        Marshal.FreeHGlobal(ImageEventsPtr);
        Marshal.FreeHGlobal(ActivityEventsPtr);
        Marshal.FreeHGlobal(RelationshipEventsPtr);
        Marshal.FreeHGlobal(LobbyEventsPtr);
        Marshal.FreeHGlobal(NetworkEventsPtr);
        Marshal.FreeHGlobal(OverlayEventsPtr);
        Marshal.FreeHGlobal(StorageEventsPtr);
        Marshal.FreeHGlobal(StoreEventsPtr);
        Marshal.FreeHGlobal(VoiceEventsPtr);
        Marshal.FreeHGlobal(AchievementEventsPtr);
        if (setLogHook.HasValue)
        {
            setLogHook.Value.Free();
        }
    }

    public void RunCallbacks()
    {
        var res = Methods.RunCallbacks(MethodsPtr);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
    }

    [MonoPInvokeCallback]
    private static void SetLogHookCallbackImpl(nint ptr, LogLevel level, string message)
    {
        GCHandle h = GCHandle.Fromnint(ptr);
        SetLogHookHandler callback = (SetLogHookHandler)h.Target;
        callback(level, message);
    }

    public void SetLogHook(LogLevel minLevel, SetLogHookHandler callback)
    {
        if (setLogHook.HasValue)
        {
            setLogHook.Value.Free();
        }
        setLogHook = GCHandle.Alloc(callback);
        Methods.SetLogHook(MethodsPtr, minLevel, GCHandle.Tonint(setLogHook.Value), SetLogHookCallbackImpl);
    }

    public ApplicationManager GetApplicationManager()
    {
        if (ApplicationManagerInstance == null)
        {
            ApplicationManagerInstance = new ApplicationManager(
              Methods.GetApplicationManager(MethodsPtr),
              ApplicationEventsPtr,
              ref ApplicationEvents
            );
        }
        return ApplicationManagerInstance;
    }

    public UserManager GetUserManager()
    {
        if (UserManagerInstance == null)
        {
            UserManagerInstance = new UserManager(
              Methods.GetUserManager(MethodsPtr),
              UserEventsPtr,
              ref UserEvents
            );
        }
        return UserManagerInstance;
    }

    public ImageManager GetImageManager()
    {
        if (ImageManagerInstance == null)
        {
            ImageManagerInstance = new ImageManager(
              Methods.GetImageManager(MethodsPtr),
              ImageEventsPtr,
              ref ImageEvents
            );
        }
        return ImageManagerInstance;
    }

    public ActivityManager GetActivityManager()
    {
        if (ActivityManagerInstance == null)
        {
            ActivityManagerInstance = new ActivityManager(
              Methods.GetActivityManager(MethodsPtr),
              ActivityEventsPtr,
              ref ActivityEvents
            );
        }
        return ActivityManagerInstance;
    }

    public RelationshipManager GetRelationshipManager()
    {
        if (RelationshipManagerInstance == null)
        {
            RelationshipManagerInstance = new RelationshipManager(
              Methods.GetRelationshipManager(MethodsPtr),
              RelationshipEventsPtr,
              ref RelationshipEvents
            );
        }
        return RelationshipManagerInstance;
    }

    public LobbyManager GetLobbyManager()
    {
        if (LobbyManagerInstance == null)
        {
            LobbyManagerInstance = new LobbyManager(
              Methods.GetLobbyManager(MethodsPtr),
              LobbyEventsPtr,
              ref LobbyEvents
            );
        }
        return LobbyManagerInstance;
    }

    public NetworkManager GetNetworkManager()
    {
        if (NetworkManagerInstance == null)
        {
            NetworkManagerInstance = new NetworkManager(
              Methods.GetNetworkManager(MethodsPtr),
              NetworkEventsPtr,
              ref NetworkEvents
            );
        }
        return NetworkManagerInstance;
    }

    public OverlayManager GetOverlayManager()
    {
        if (OverlayManagerInstance == null)
        {
            OverlayManagerInstance = new OverlayManager(
              Methods.GetOverlayManager(MethodsPtr),
              OverlayEventsPtr,
              ref OverlayEvents
            );
        }
        return OverlayManagerInstance;
    }

    public StorageManager GetStorageManager()
    {
        if (StorageManagerInstance == null)
        {
            StorageManagerInstance = new StorageManager(
              Methods.GetStorageManager(MethodsPtr),
              StorageEventsPtr,
              ref StorageEvents
            );
        }
        return StorageManagerInstance;
    }

    public StoreManager GetStoreManager()
    {
        if (StoreManagerInstance == null)
        {
            StoreManagerInstance = new StoreManager(
              Methods.GetStoreManager(MethodsPtr),
              StoreEventsPtr,
              ref StoreEvents
            );
        }
        return StoreManagerInstance;
    }

    public VoiceManager GetVoiceManager()
    {
        if (VoiceManagerInstance == null)
        {
            VoiceManagerInstance = new VoiceManager(
              Methods.GetVoiceManager(MethodsPtr),
              VoiceEventsPtr,
              ref VoiceEvents
            );
        }
        return VoiceManagerInstance;
    }

    public AchievementManager GetAchievementManager()
    {
        if (AchievementManagerInstance == null)
        {
            AchievementManagerInstance = new AchievementManager(
              Methods.GetAchievementManager(MethodsPtr),
              AchievementEventsPtr,
              ref AchievementEvents
            );
        }
        return AchievementManagerInstance;
    }
}