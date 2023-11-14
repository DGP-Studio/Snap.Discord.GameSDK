using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK;

public class OverlayManager
{
    private unsafe readonly OverlayMethods* MethodsPtr;

    internal unsafe OverlayManager(OverlayMethods* ptr, OverlayEvents* eventsPtr, ref OverlayEvents events)
    {
        ResultException.ThrowIfNull(ptr);
        InitEvents(eventsPtr, ref events);
        MethodsPtr = ptr;
    }

    private static unsafe void InitEvents(OverlayEvents* eventsPtr, ref OverlayEvents events)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void OnToggleImpl(nint ptr, bool locked)
        {
            DiscordGCHandle.Get(ptr).OverlayManagerInstance?.OnToggle(locked);
        }

        events.OnToggle = ToggleHandler.Create(&OnToggleImpl);
        *eventsPtr = events;
    }

    public unsafe bool IsEnabled()
    {
        bool ret = default;
        MethodsPtr->IsEnabled.Invoke(MethodsPtr, &ret);
        return ret;
    }

    public unsafe bool IsLocked()
    {
        bool ret = default;
        MethodsPtr->IsLocked.Invoke(MethodsPtr, &ret);
        return ret;
    }

    public unsafe void SetLocked(bool locked, SetLockedHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void SetLockedCallbackImpl(SetLockedHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->SetLocked.Invoke(MethodsPtr, locked, callback, SetLockedCallback.Create(&SetLockedCallbackImpl));
    }

    public unsafe void OpenActivityInvite(ActivityActionType type, OpenActivityInviteHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void OpenActivityInviteCallbackImpl(OpenActivityInviteHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        GCHandle wrapped = GCHandle.Alloc(callback);
        MethodsPtr->OpenActivityInvite.Invoke(MethodsPtr, type, callback, OpenActivityInviteCallback.Create(&OpenActivityInviteCallbackImpl));
    }

    public unsafe void OpenGuildInvite(string code, OpenGuildInviteHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void OpenGuildInviteCallbackImpl(OpenGuildInviteHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        byte[] codeBytes = Encoding.UTF8.GetBytes(code);
        fixed (byte* pCode = codeBytes)
        {
            MethodsPtr->OpenGuildInvite.Invoke(MethodsPtr, pCode, callback, OpenGuildInviteCallback.Create(&OpenGuildInviteCallbackImpl));
        }
    }

    public unsafe void OpenVoiceSettings(OpenVoiceSettingsHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void OpenVoiceSettingsCallbackImpl(OpenVoiceSettingsHandler ptr, Result result)
        {
            ptr.Invoke(result);
        }

        MethodsPtr->OpenVoiceSettings.Invoke(MethodsPtr, callback, OpenVoiceSettingsCallback.Create(&OpenVoiceSettingsCallbackImpl));
    }

    public unsafe void InitDrawingDxgi(nint swapchain, bool useMessageForwarding)
    {
        MethodsPtr->InitDrawingDxgi.Invoke(MethodsPtr, swapchain, useMessageForwarding).ThrowOnFailure();
    }

    public unsafe void OnPresent()
    {
        MethodsPtr->OnPresent.Invoke(MethodsPtr);
    }

    public unsafe void ForwardMessage(nint message)
    {
        MethodsPtr->ForwardMessage.Invoke(MethodsPtr, message);
    }

    public unsafe void KeyEvent(bool down, string keyCode, KeyVariant variant)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(keyCode);
        fixed (byte* pKeyCode = bytes)
        {
            MethodsPtr->KeyEvent.Invoke(MethodsPtr, down, pKeyCode, variant);
        }
    }

    public unsafe void CharEvent(string character)
    {
        byte[] characterBytes = Encoding.UTF8.GetBytes(character);
        fixed (byte* pCharacter = characterBytes)
        {
            MethodsPtr->CharEvent.Invoke(MethodsPtr, pCharacter);
        }
    }

    public unsafe void MouseButtonEvent(byte down, int clickCount, MouseButton which, int x, int y)
    {
        MethodsPtr->MouseButtonEvent.Invoke(MethodsPtr, down, clickCount, which, x, y);
    }

    public unsafe void MouseMotionEvent(int x, int y)
    {
        MethodsPtr->MouseMotionEvent.Invoke(MethodsPtr, x, y);
    }

    public unsafe void ImeCommitText(string text)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        fixed (byte* pText = textBytes)
        {
            MethodsPtr->ImeCommitText.Invoke(MethodsPtr, pText);
        }
    }

    public unsafe void ImeSetComposition(string text, ImeUnderline underlines, int from, int to)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        fixed (byte* pText = textBytes)
        {
            MethodsPtr->ImeSetComposition.Invoke(MethodsPtr, pText, &underlines, from, to);
        }
    }

    public unsafe void ImeCancelComposition()
    {
        MethodsPtr->ImeCancelComposition.Invoke(MethodsPtr);
    }

    public unsafe void SetImeCompositionRangeCallback(SetImeCompositionRangeCallbackHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static unsafe void SetImeCompositionRangeCallbackCallbackImpl(SetImeCompositionRangeCallbackHandler ptr, int from, int to, Rect* bounds)
        {
            ptr.Invoke(from, to, bounds);
        }

        MethodsPtr->SetImeCompositionRangeCallback.Invoke(MethodsPtr, callback, SetImeCompositionRangeCallbackCallback.Create(&SetImeCompositionRangeCallbackCallbackImpl));
    }

    public unsafe void SetImeSelectionBoundsCallback(SetImeSelectionBoundsCallbackHandler callback)
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        static void SetImeSelectionBoundsCallbackCallbackImpl(SetImeSelectionBoundsCallbackHandler ptr, Rect anchor, Rect focus, bool isAnchorFirst)
        {
            ptr.Invoke(anchor, focus, isAnchorFirst);
        }

        MethodsPtr->SetImeSelectionBoundsCallback.Invoke(MethodsPtr, callback, SetImeSelectionBoundsCallbackCallback.Create(&SetImeSelectionBoundsCallbackCallbackImpl));
    }

    public unsafe bool IsPointInsideClickZone(int x, int y)
    {
        return MethodsPtr->IsPointInsideClickZone.Invoke(MethodsPtr, x, y);
    }

    protected virtual void OnToggle(bool locked)
    {
    }
}