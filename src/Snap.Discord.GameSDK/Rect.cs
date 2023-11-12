using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public partial struct Rect
{
    public Int32 Left;

    public Int32 Top;

    public Int32 Right;

    public Int32 Bottom;
}
