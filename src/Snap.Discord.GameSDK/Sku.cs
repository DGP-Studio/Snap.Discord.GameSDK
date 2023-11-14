namespace Snap.Discord.GameSDK;

public struct Sku
{
    public long Id;

    public SkuType Type;

    public unsafe fixed byte Name[256];

    public SkuPrice Price;
}