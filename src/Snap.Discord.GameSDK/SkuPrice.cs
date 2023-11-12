using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public partial struct SkuPrice
{
    public UInt32 Amount;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
    public string Currency;
}
