using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public partial struct FileStat
{
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string Filename;

    public UInt64 Size;

    public UInt64 LastModified;
}
