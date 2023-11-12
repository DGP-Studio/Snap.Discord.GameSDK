using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct Entitlement
{
    public long Id;

    public EntitlementType Type;

    public long SkuId;
}
