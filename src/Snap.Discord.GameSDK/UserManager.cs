using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

public class UserManager
{
    private unsafe readonly UserMethods* MethodsPtr;

    internal unsafe UserManager(UserMethods* ptr, UserEvents* eventsPtr, ref UserEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(UserEvents* eventsPtr, ref UserEvents events)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void OnCurrentUserUpdateImpl(nint ptr)
        {
            DiscordGCHandle.Get(ptr).UserManagerInstance.OnCurrentUserUpdate();
        }

        events.OnCurrentUserUpdate = CurrentUserUpdateHandler.Create(&OnCurrentUserUpdateImpl);
        *eventsPtr = events;
    }

    public unsafe User GetCurrentUser()
    {
        User ret = default;
        MethodsPtr->GetCurrentUser.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe void GetUser(long userId, GetUserHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void GetUserCallbackImpl(GetUserHandler ptr, Result result, User* user)
        {
            ptr.Invoke(result, user);
        }

        MethodsPtr->GetUser.Invoke(MethodsPtr, userId, callback, GetUserCallback.Create(&GetUserCallbackImpl));
    }

    public unsafe PremiumType GetCurrentUserPremiumType()
    {
        PremiumType ret = default;
        MethodsPtr->GetCurrentUserPremiumType.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    public unsafe bool CurrentUserHasFlag(UserFlag flag)
    {
        bool ret = default;
        MethodsPtr->CurrentUserHasFlag.Invoke(MethodsPtr, flag, &ret).ThrowOnFailure();
        return ret;
    }

    protected virtual void OnCurrentUserUpdate()
    {
    }
}