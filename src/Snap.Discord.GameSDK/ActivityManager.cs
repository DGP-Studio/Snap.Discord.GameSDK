using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

public class ActivityManager
{
    private unsafe readonly ActivityMethods* MethodsPtr;

    internal unsafe ActivityManager(ActivityMethods* ptr, ActivityEvents* eventsPtr, ref ActivityEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(ActivityEvents* eventsPtr, ref ActivityEvents events)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void OnActivityJoinImpl(nint ptr, byte* secret)
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(secret);
            string secretString = Encoding.UTF8.GetString(bytes);

            DiscordGCHandle.Get(ptr).ActivityManagerInstance.OnActivityJoin(secretString);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void OnActivitySpectateImpl(nint ptr, byte* secret)
        {
            ReadOnlySpan<byte> bytes = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(secret);
            string secretString = Encoding.UTF8.GetString(bytes);

            DiscordGCHandle.Get(ptr).ActivityManagerInstance.OnActivitySpectate(secretString);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void OnActivityJoinRequestImpl(nint ptr, User* user)
        {
            DiscordGCHandle.Get(ptr).ActivityManagerInstance.OnActivityJoinRequest(ref *user);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void OnActivityInviteImpl(nint ptr, ActivityActionType type, User* user, Activity* activity)
        {
            DiscordGCHandle.Get(ptr).ActivityManagerInstance.OnActivityInvite(type, ref *user, ref *activity);
        }

        events.OnActivityJoin = ActivityJoinHandler.Create(&OnActivityJoinImpl);
        events.OnActivitySpectate = ActivitySpectateHandler.Create(&OnActivitySpectateImpl);
        events.OnActivityJoinRequest = ActivityJoinRequestHandler.Create(&OnActivityJoinRequestImpl);
        events.OnActivityInvite = ActivityInviteHandler.Create(&OnActivityInviteImpl);
        *eventsPtr = events;
    }

    public unsafe void RegisterCommand(string command)
    {
        byte[] commandBytes = Encoding.UTF8.GetBytes(command);
        fixed (byte* pCommand = commandBytes)
        {
            MethodsPtr->RegisterCommand.Invoke(MethodsPtr, pCommand).ThrowOnFailure();
        }
    }

    public unsafe void RegisterSteam(uint steamId)
    {
        MethodsPtr->RegisterSteam.Invoke(MethodsPtr, steamId).ThrowOnFailure();
    }

    public unsafe void UpdateActivity(Activity activity, UpdateActivityHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void UpdateActivityCallbackImpl(UpdateActivityHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->UpdateActivity.Invoke(MethodsPtr, &activity, callback, UpdateActivityCallback.Create(&UpdateActivityCallbackImpl));
    }

    public unsafe void ClearActivity(ClearActivityHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void ClearActivityCallbackImpl(ClearActivityHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->ClearActivity.Invoke(MethodsPtr, callback, ClearActivityCallback.Create(&ClearActivityCallbackImpl));
    }

    public unsafe void SendRequestReply(long userId, ActivityJoinRequestReply reply, SendRequestReplyHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void SendRequestReplyCallbackImpl(SendRequestReplyHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->SendRequestReply.Invoke(MethodsPtr, userId, reply, callback, SendRequestReplyCallback.Create(&SendRequestReplyCallbackImpl));
    }

    public unsafe void SendInvite(long userId, ActivityActionType type, string content, SendInviteHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
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

    public unsafe void AcceptInvite(long userId, AcceptInviteHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void AcceptInviteCallbackImpl(AcceptInviteHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->AcceptInvite.Invoke(MethodsPtr, userId, callback, AcceptInviteCallback.Create(&AcceptInviteCallbackImpl));
    }

    protected virtual void OnActivityJoin(string secret)
    {
    }

    protected virtual void OnActivitySpectate(string secret)
    {
    }

    protected virtual void OnActivityJoinRequest(ref User user)
    {
    }

    protected virtual void OnActivityInvite(ActivityActionType type, ref User user, ref Activity activity)
    {
    }
}