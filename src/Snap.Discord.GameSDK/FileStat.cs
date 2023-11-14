namespace Snap.Discord.GameSDK;

public struct FileStat
{
    public unsafe fixed byte Filename[260];

    public ulong Size;

    public ulong LastModified;
}
