namespace Snap.Discord.GameSDK;

public struct InputMode
{
    public InputModeType Type;

    public unsafe fixed byte Shortcut[256];
}