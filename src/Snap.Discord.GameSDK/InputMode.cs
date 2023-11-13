using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct InputMode
{
    public InputModeType Type;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
    public string Shortcut;
}
