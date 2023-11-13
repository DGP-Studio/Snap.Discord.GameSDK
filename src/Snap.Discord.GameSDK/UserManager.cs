using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class UserManager
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct FFIEvents
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void CurrentUserUpdateHandler(IntPtr ptr);

        internal CurrentUserUpdateHandler OnCurrentUserUpdate;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct FFIMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetCurrentUserMethod(IntPtr methodsPtr, ref User currentUser);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void GetUserCallback(IntPtr ptr, Result result, ref User user);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void GetUserMethod(IntPtr methodsPtr, Int64 userId, IntPtr callbackData, GetUserCallback callback);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result GetCurrentUserPremiumTypeMethod(IntPtr methodsPtr, ref PremiumType premiumType);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate Result CurrentUserHasFlagMethod(IntPtr methodsPtr, UserFlag flag, ref bool hasFlag);

        internal GetCurrentUserMethod GetCurrentUser;

        internal GetUserMethod GetUser;

        internal GetCurrentUserPremiumTypeMethod GetCurrentUserPremiumType;

        internal CurrentUserHasFlagMethod CurrentUserHasFlag;
    }

    public delegate void GetUserHandler(Result result, ref User user);

    public delegate void CurrentUserUpdateHandler();

    private IntPtr MethodsPtr;

    private Object MethodsStructure;

    private FFIMethods Methods
    {
        get
        {
            if (MethodsStructure == null)
            {
                MethodsStructure = Marshal.PtrToStructure(MethodsPtr, typeof(FFIMethods));
            }
            return (FFIMethods)MethodsStructure;
        }

    }

    public event CurrentUserUpdateHandler OnCurrentUserUpdate;

    internal UserManager(IntPtr ptr, IntPtr eventsPtr, ref FFIEvents events)
    {
        if (eventsPtr == IntPtr.Zero)
        {
            throw new ResultException(Result.InternalError);
        }
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
        if (MethodsPtr == IntPtr.Zero)
        {
            throw new ResultException(Result.InternalError);
        }
    }

    private void InitEvents(IntPtr eventsPtr, ref FFIEvents events)
    {
        events.OnCurrentUserUpdate = OnCurrentUserUpdateImpl;
        Marshal.StructureToPtr(events, eventsPtr, false);
    }

    public User GetCurrentUser()
    {
        var ret = new User();
        var res = Methods.GetCurrentUser(MethodsPtr, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    [MonoPInvokeCallback]
    private static void GetUserCallbackImpl(IntPtr ptr, Result result, ref User user)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        GetUserHandler callback = (GetUserHandler)h.Target;
        h.Free();
        callback(result, ref user);
    }

    public void GetUser(Int64 userId, GetUserHandler callback)
    {
        GCHandle wrapped = GCHandle.Alloc(callback);
        Methods.GetUser(MethodsPtr, userId, GCHandle.ToIntPtr(wrapped), GetUserCallbackImpl);
    }

    public PremiumType GetCurrentUserPremiumType()
    {
        var ret = new PremiumType();
        var res = Methods.GetCurrentUserPremiumType(MethodsPtr, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    public bool CurrentUserHasFlag(UserFlag flag)
    {
        var ret = new bool();
        var res = Methods.CurrentUserHasFlag(MethodsPtr, flag, ref ret);
        if (res != Result.Ok)
        {
            throw new ResultException(res);
        }
        return ret;
    }

    [MonoPInvokeCallback]
    private static void OnCurrentUserUpdateImpl(IntPtr ptr)
    {
        GCHandle h = GCHandle.FromIntPtr(ptr);
        Discord d = (Discord)h.Target;
        if (d.UserManagerInstance.OnCurrentUserUpdate != null)
        {
            d.UserManagerInstance.OnCurrentUserUpdate.Invoke();
        }
    }
}
