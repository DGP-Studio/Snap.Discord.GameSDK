using Snap.Discord.GameSDK.ABI;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Snap.Discord.GameSDK;

public partial class ActivityManager
{
    private unsafe readonly ActivityMethods* MethodsPtr;
    private static readonly ConcurrentDictionary<string, ManualResetEventSlim> callbackWaits = [];
    private static readonly ConcurrentDictionary<string, Result> callbackResults = [];

    public unsafe ActivityManager(ActivityMethods* ptr, ActivityEvents* eventsPtr)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(ActivityEvents* eventsPtr)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnActivityJoinImpl(nint ptr, byte* secret)
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(secret);
            string secretString = Encoding.UTF8.GetString(bytes);

            DiscordGCHandle.Get(ptr).ActivityManagerInstance?.OnActivityJoin(secretString);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnActivitySpectateImpl(nint ptr, byte* secret)
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(secret);
            string secretString = Encoding.UTF8.GetString(bytes);

            DiscordGCHandle.Get(ptr).ActivityManagerInstance?.OnActivitySpectate(secretString);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnActivityJoinRequestImpl(nint ptr, User* user)
        {
            DiscordGCHandle.Get(ptr).ActivityManagerInstance?.OnActivityJoinRequest(ref *user);
        }

        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void OnActivityInviteImpl(nint ptr, ActivityActionType type, User* user, Activity* activity)
        {
            DiscordGCHandle.Get(ptr).ActivityManagerInstance?.OnActivityInvite(type, ref *user, ref *activity);
        }

        eventsPtr->OnActivityJoin = ActivityJoinHandler.Create(&OnActivityJoinImpl);
        eventsPtr->OnActivitySpectate = ActivitySpectateHandler.Create(&OnActivitySpectateImpl);
        eventsPtr->OnActivityJoinRequest = ActivityJoinRequestHandler.Create(&OnActivityJoinRequestImpl);
        eventsPtr->OnActivityInvite = ActivityInviteHandler.Create(&OnActivityInviteImpl);
    }

    /// <summary>
    /// <para>
    /// Registers a command by which Discord can launch your game. This might be a custom protocol, like
    /// <code>my-awesome-game://</code>
    /// or a path to an executable. It also supports any launch parameters that may be needed, like
    /// <code>game.exe --full-screen --no-hax</code>
    /// </para>
    /// <para>
    /// On macOS, due to the way Discord registers executables, your game needs to be bundled for this command to work. That means it should be a
    /// <code>.app</code>
    /// </para>
    /// </summary>
    /// <param name="command">the command to register</param>
    public unsafe void RegisterCommand(string command = default!)
    {
        if (command is null)
        {
            MethodsPtr->RegisterCommand.Invoke(MethodsPtr, default).ThrowOnFailure();
            return;
        }

        byte[] commandBytes = Encoding.UTF8.GetBytes(command);
        fixed (byte* pCommand = commandBytes)
        {
            MethodsPtr->RegisterCommand.Invoke(MethodsPtr, pCommand).ThrowOnFailure();
        }
    }

    /// <summary>
    /// Used if you are distributing this SDK on Steam. Registers your game's Steam app id for the protocol <code>steam://run-game-id/&lt;id&gt;</code>
    /// </summary>
    /// <param name="steamId">your game's Steam app id</param>
    public unsafe void RegisterSteam(uint steamId)
    {
        MethodsPtr->RegisterSteam.Invoke(MethodsPtr, steamId).ThrowOnFailure();
    }

    /// <summary>
    /// <para>
    /// Sets a user's presence in Discord to a new activity. This has a rate limit of 5 updates per 20 seconds.
    /// </para>
    /// <para>
    /// It is possible for users to hide their presence on Discord (User Settings -> Game Activity). Presence set through this SDK may not be visible when this setting is toggled off.
    /// </para>
    /// </summary>
    /// <param name="activity">the new activity for the user</param>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void UpdateActivity(Activity activity, UpdateActivityHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void UpdateActivityCallbackImpl(UpdateActivityHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->UpdateActivity.Invoke(MethodsPtr, &activity, callback, UpdateActivityCallback.Create(&UpdateActivityCallbackImpl));
    }

    /// <summary>
    /// Clear's a user's presence in Discord to make it show nothing.
    /// </summary>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void ClearActivity(ClearActivityHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void ClearActivityCallbackImpl(ClearActivityHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->ClearActivity.Invoke(MethodsPtr, callback, ClearActivityCallback.Create(&ClearActivityCallbackImpl));
    }

    /// <summary>
    /// Sends a reply to an Ask to Join request.
    /// </summary>
    /// <param name="userId">the user id of the person who asked to join</param>
    /// <param name="reply">No, Yes, or Ignore</param>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void SendRequestReply(long userId, ActivityJoinRequestReply reply, SendRequestReplyHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void SendRequestReplyCallbackImpl(SendRequestReplyHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->SendRequestReply.Invoke(MethodsPtr, userId, reply, callback, SendRequestReplyCallback.Create(&SendRequestReplyCallbackImpl));
    }

    /// <summary>
    /// Sends a game invite to a given user. If you do not have a valid activity with all the required fields, this call will error. See <see href="https://discord.com/developers/docs/game-sdk/activities#activity-action-field-requirements">Activity Action Field Requirements</see> for the fields required to have join and spectate invites function properly.
    /// </summary>
    /// <param name="userId">the id of the user to invite</param>
    /// <param name="type">marks the invite as an invitation to join or spectate</param>
    /// <param name="content">a message to send along with the invite</param>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void SendInvite(long userId, ActivityActionType type, string content, SendInviteHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void SendInviteCallbackImpl(SendInviteHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        byte[] contentBytes = Encoding.UTF8.GetBytes(content);
        fixed (byte* pContent = contentBytes)
        {
            MethodsPtr->SendInvite.Invoke(MethodsPtr, userId, type, pContent, callback, SendInviteCallback.Create(&SendInviteCallbackImpl));
        }
    }

    /// <summary>
    /// Accepts a game invitation from a given userId.
    /// </summary>
    /// <param name="userId">the id of the user who invited you</param>
    /// <param name="callback"></param>
    [AsyncCallback]
    public unsafe void AcceptInvite(long userId, AcceptInviteHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
        static unsafe void AcceptInviteCallbackImpl(AcceptInviteHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->AcceptInvite.Invoke(MethodsPtr, userId, callback, AcceptInviteCallback.Create(&AcceptInviteCallbackImpl));
    }

    /// <summary>
    /// Fires when a user accepts a game chat invite or receives confirmation from Asking to Join.
    /// </summary>
    /// <param name="secret">the secret to join the user's game</param>
    protected virtual void OnActivityJoin(string secret)
    {
    }

    /// <summary>
    /// Fires when a user accepts a spectate chat invite or clicks the Spectate button on a user's profile.
    /// </summary>
    /// <param name="secret">the secret to join the user's game as a spectator</param>
    protected virtual void OnActivitySpectate(string secret)
    {
    }

    /// <summary>
    /// Fires when a user asks to join the current user's game.
    /// </summary>
    /// <param name="user">the user asking to join</param>
    protected virtual void OnActivityJoinRequest(ref User user)
    {
    }

    /// <summary>
    /// Fires when the user receives a join or spectate invite.
    /// </summary>
    /// <param name="type">whether this invite is to join or spectate</param>
    /// <param name="user">the user sending the invite</param>
    /// <param name="activity">the inviting user's current activity</param>
    protected virtual void OnActivityInvite(ActivityActionType type, ref User user, ref Activity activity)
    {
    }
}