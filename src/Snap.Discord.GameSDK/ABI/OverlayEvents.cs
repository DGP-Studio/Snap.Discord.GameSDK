namespace Snap.Discord.GameSDK.ABI;

public struct OverlayEvents
{
    internal unsafe delegate* unmanaged[Stdcall]<nint, bool, void> OnToggle;
}
