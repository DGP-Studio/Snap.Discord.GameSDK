using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

/// <summary>
/// This manager helps retrieve basic user information for any user on Discord.
/// </summary>
public class UserManager
{
    private unsafe readonly UserMethods* MethodsPtr;

    public unsafe UserManager(UserMethods* ptr, UserEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(UserEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static void OnCurrentUserUpdateImpl(nint ptr)
        {
            DiscordGCHandle.Get(ptr).UserManagerInstance?.OnCurrentUserUpdate();
        }

        eventsPtr->OnCurrentUserUpdate = CurrentUserUpdateHandler.Create(&OnCurrentUserUpdateImpl);
    }

    /// <summary>
    /// Before calling this function, you'll need to wait for the <see cref="OnCurrentUserUpdate"/> callback to fire after instantiating the User manager.
    /// <para>
    /// Fetch information about the currently connected user account. If you're interested in getting more detailed information about a user—for example, their email—check out our
    /// <see href="https://discord.com/developers/docs/resources/user#get-current-user">GetCurrentUser</see> API endpoint. You'll want to call this with an authorization header of
    /// <code>Bearer &lt;token&gt;</code>
    /// where &lt;token&gt; is the token retrieved from a standard OAuth2 Authorization Code Grant flow.
    /// </para>
    /// </summary>
    /// <returns></returns>
    public unsafe User GetCurrentUser()
    {
        User ret = default;
        MethodsPtr->GetCurrentUser.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    /// <summary>
    /// Get user information for a given id.
    /// </summary>
    /// <param name="userId">the id of the user to fetch</param>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void GetUser(long userId, GetUserHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void GetUserCallbackImpl(GetUserHandler ptr, Result result, User* user)
        {
            ptr.Invoke(result, user);
        }

        MethodsPtr->GetUser.Invoke(MethodsPtr, userId, callback, GetUserCallback.Create(&GetUserCallbackImpl));
    }

    /// <summary>
    /// Get the <see cref="PremiumType"/> for the currently connected user.
    /// </summary>
    /// <returns></returns>
    public unsafe PremiumType GetCurrentUserPremiumType()
    {
        PremiumType ret = default;
        MethodsPtr->GetCurrentUserPremiumType.Invoke(MethodsPtr, &ret).ThrowOnFailure();
        return ret;
    }

    /// <summary>
    /// See whether or not the current user has a certain UserFlag on their account.
    /// </summary>
    /// <param name="flag">the flag to check on the user's account</param>
    /// <returns></returns>
    public unsafe bool CurrentUserHasFlag(UserFlag flag)
    {
        bool ret = default;
        MethodsPtr->CurrentUserHasFlag.Invoke(MethodsPtr, flag, &ret).ThrowOnFailure();
        return ret;
    }

    /// <summary>
    /// Fires when the <see cref="User"/> struct of the currently connected user changes. They may have changed their avatar, username, or something else.
    /// </summary>
    protected virtual void OnCurrentUserUpdate()
    {
    }
}