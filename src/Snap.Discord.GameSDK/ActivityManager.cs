using Snap.Discord.GameSDK.ABI;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

public class ActivityManager
{
    private unsafe readonly ActivityMethods* MethodsPtr;

    internal unsafe ActivityManager(ActivityMethods* ptr, nint eventsPtr, ref ActivityEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(nint eventsPtr, ref ActivityEvents events)
    {
        events.OnActivityJoin = ActivityJoinHandler.Create(&OnActivityJoinImpl);
        events.OnActivitySpectate = ActivitySpectateHandler.Create(&OnActivitySpectateImpl);
        events.OnActivityJoinRequest = ActivityJoinRequestHandler.Create(&OnActivityJoinRequestImpl);
        events.OnActivityInvite = ActivityInviteHandler.Create(&OnActivityInviteImpl);
        *(ActivityEvents*)eventsPtr = events;
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

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void UpdateActivityCallbackImpl(UpdateActivityHandler ptr, Result result)
    {
        ptr.Invoke(result);
    }

    public unsafe void UpdateActivity(Activity activity, UpdateActivityHandler callback)
    {
        MethodsPtr->UpdateActivity.Invoke(MethodsPtr, &activity, callback, UpdateActivityCallback.Create(&UpdateActivityCallbackImpl));
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void ClearActivityCallbackImpl(ClearActivityHandler ptr, Result result)
    {
        ptr.Invoke(result);
    }

    public unsafe void ClearActivity(ClearActivityHandler callback)
    {
        MethodsPtr->ClearActivity.Invoke(MethodsPtr, callback, ClearActivityCallback.Create(&ClearActivityCallbackImpl));
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void SendRequestReplyCallbackImpl(SendRequestReplyHandler ptr, Result result)
    {
        ptr.Invoke(result);
    }

    public unsafe void SendRequestReply(long userId, ActivityJoinRequestReply reply, SendRequestReplyHandler callback)
    {
        MethodsPtr->SendRequestReply.Invoke(MethodsPtr, userId, reply, callback, SendRequestReplyCallback.Create(&SendRequestReplyCallbackImpl));
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void SendInviteCallbackImpl(SendInviteHandler ptr, Result result)
    {
        ptr.Invoke(result);
    }

    public unsafe void SendInvite(long userId, ActivityActionType type, string content, SendInviteHandler callback)
    {
        byte[] contentBytes = Encoding.UTF8.GetBytes(content);
        fixed (byte* pContent = contentBytes)
        {
            MethodsPtr->SendInvite.Invoke(MethodsPtr, userId, type, pContent, callback, SendInviteCallback.Create(&SendInviteCallbackImpl));
        }
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void AcceptInviteCallbackImpl(AcceptInviteHandler ptr, Result result)
    {
        ptr.Invoke(result);
    }

    public unsafe void AcceptInvite(long userId, AcceptInviteHandler callback)
    {
        MethodsPtr->AcceptInvite.Invoke(MethodsPtr, userId, callback, AcceptInviteCallback.Create(&AcceptInviteCallbackImpl));
    }

    protected virtual void OnActivityJoin(string secret)
    {
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnActivityJoinImpl(nint ptr, byte* secret)
    {
        ReadOnlySpan<byte> bytes = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(secret);
        string secretString = Encoding.UTF8.GetString(bytes);

        Discord d = DiscordGCHandle.Get(ptr);
        d.ActivityManagerInstance.OnActivityJoin(secretString);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnActivitySpectateImpl(nint ptr, byte* secret)
    {
        ReadOnlySpan<byte> bytes = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(secret);
        string secretString = Encoding.UTF8.GetString(bytes);

        Discord d = DiscordGCHandle.Get(ptr);
        d.ActivityManagerInstance.OnActivitySpectate?.Invoke(secretString);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnActivityJoinRequestImpl(nint ptr, User* user)
    {
        Discord d = DiscordGCHandle.Get(ptr);
        d.ActivityManagerInstance.OnActivityJoinRequest?.Invoke(ref *user);
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static unsafe void OnActivityInviteImpl(nint ptr, ActivityActionType type, User* user, Activity* activity)
    {
        Discord d = DiscordGCHandle.Get(ptr);
        d.ActivityManagerInstance.OnActivityInvite?.Invoke(type, ref *user, ref *activity);
    }
}