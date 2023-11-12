namespace Snap.Discord.GameSDK;

public struct User
{
    public long Id;

    public unsafe fixed byte Username[256];

    public unsafe fixed byte Discriminator[8];

    public unsafe fixed byte Avatar[128];

    public bool Bot;
}
