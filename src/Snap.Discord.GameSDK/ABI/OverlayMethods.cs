namespace Snap.Discord.GameSDK.ABI;

internal struct OverlayMethods
{
    internal IsEnabledMethod IsEnabled;
    internal IsLockedMethod IsLocked;
    internal SetLockedMethod SetLocked;
    internal OpenActivityInviteMethod OpenActivityInvite;
    internal OpenGuildInviteMethod OpenGuildInvite;
    internal OpenVoiceSettingsMethod OpenVoiceSettings;
    internal InitDrawingDxgiMethod InitDrawingDxgi;
    internal OnPresentMethod OnPresent;
    internal ForwardMessageMethod ForwardMessage;
    internal KeyEventMethod KeyEvent;
    internal CharEventMethod CharEvent;
    internal MouseButtonEventMethod MouseButtonEvent;
    internal MouseMotionEventMethod MouseMotionEvent;
    internal ImeCommitTextMethod ImeCommitText;
    internal ImeSetCompositionMethod ImeSetComposition;
    internal ImeCancelCompositionMethod ImeCancelComposition;
    internal SetImeCompositionRangeCallbackMethod SetImeCompositionRangeCallback;
    internal SetImeSelectionBoundsCallbackMethod SetImeSelectionBoundsCallback;
    internal IsPointInsideClickZoneMethod IsPointInsideClickZone;
}