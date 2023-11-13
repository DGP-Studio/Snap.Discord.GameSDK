using Snap.Discord.GameSDK;

namespace Snap.Discord.GameSDK.ABI;

public struct OverlayMethods
{
    // void IsEnabledMethod(IntPtr methodsPtr, ref bool enabled)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> IsEnabled;

    // void IsLockedMethod(IntPtr methodsPtr, ref bool locked)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> IsLocked;

    // void SetLockedCallback(IntPtr ptr, Result result)
    // void SetLockedMethod(IntPtr methodsPtr, bool locked, IntPtr callbackData, SetLockedCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> SetLocked;

    // void OpenActivityInviteCallback(IntPtr ptr, Result result)
    // void OpenActivityInviteMethod(IntPtr methodsPtr, ActivityActionType type, IntPtr callbackData, OpenActivityInviteCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, ActivityActionType, delegate* unmanaged[Stdcall]<Result, void>, delegate* unmanaged[Stdcall]<delegate* unmanaged[Stdcall]<Result, void>, Result, void>, void> OpenActivityInvite;

    // void OpenGuildInviteCallback(IntPtr ptr, Result result)
    // void OpenGuildInviteMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string code, IntPtr callbackData, OpenGuildInviteCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> OpenGuildInvite;

    // void OpenVoiceSettingsCallback(IntPtr ptr, Result result)
    // void OpenVoiceSettingsMethod(IntPtr methodsPtr, IntPtr callbackData, OpenVoiceSettingsCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> OpenVoiceSettings;

    // Result InitDrawingDxgiMethod(IntPtr methodsPtr, IntPtr swapchain, bool useMessageForwarding)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> InitDrawingDxgi;

    // void OnPresentMethod(IntPtr methodsPtr)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> OnPresent;

    // void ForwardMessageMethod(IntPtr methodsPtr, IntPtr message)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> ForwardMessage;

    // void KeyEventMethod(IntPtr methodsPtr, bool down, [MarshalAs(UnmanagedType.LPStr)] string keyCode, KeyVariant variant)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> KeyEvent;

    // void CharEventMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string character)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> CharEvent;

    // void MouseButtonEventMethod(IntPtr methodsPtr, byte down, Int32 clickCount, MouseButton which, Int32 x, Int32 y)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> MouseButtonEvent;

    // void MouseMotionEventMethod(IntPtr methodsPtr, Int32 x, Int32 y)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> MouseMotionEvent;

    // void ImeCommitTextMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string text)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> ImeCommitText;

    // void ImeSetCompositionMethod(IntPtr methodsPtr, [MarshalAs(UnmanagedType.LPStr)] string text, ref ImeUnderline underlines, Int32 from, Int32 to)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> ImeSetComposition;

    // void ImeCancelCompositionMethod(IntPtr methodsPtr)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> ImeCancelComposition;

    // void SetImeCompositionRangeCallbackCallback(IntPtr ptr, Int32 from, Int32 to, ref Rect bounds)
    // void SetImeCompositionRangeCallbackMethod(IntPtr methodsPtr, IntPtr callbackData, SetImeCompositionRangeCallbackCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> SetImeCompositionRangeCallback;

    // void SetImeSelectionBoundsCallbackCallback(IntPtr ptr, Rect anchor, Rect focus, bool isAnchorFirst)
    // void SetImeSelectionBoundsCallbackMethod(IntPtr methodsPtr, IntPtr callbackData, SetImeSelectionBoundsCallbackCallback callback)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> SetImeSelectionBoundsCallback;

    // bool IsPointInsideClickZoneMethod(IntPtr methodsPtr, Int32 x, Int32 y)
    internal unsafe delegate* unmanaged[Stdcall]<OverlayMethods*, bool*, void> IsPointInsideClickZone;
}