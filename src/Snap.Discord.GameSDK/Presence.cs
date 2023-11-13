using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct Presence
{
    public Status Status;

    public Activity Activity;
}
