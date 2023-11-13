using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct ImeUnderline
{
    public Int32 From;

    public Int32 To;

    public UInt32 Color;

    public UInt32 BackgroundColor;

    public bool Thick;
}
