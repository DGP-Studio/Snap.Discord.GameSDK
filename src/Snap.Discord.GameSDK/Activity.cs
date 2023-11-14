using System;

namespace Snap.Discord.GameSDK;

public struct Activity
{
    public ActivityType Type;

    public long ApplicationId;

    public unsafe fixed byte Name[128];

    public unsafe void SetName(ReadOnlySpan<byte> source)
    {
        fixed (byte* ptr = Name)
        {
            Span<byte> target = new(ptr, 128);
            target.Clear();
            source.CopyTo(target);
        }
    }

    public unsafe fixed byte State[128];

    public unsafe void SetState(ReadOnlySpan<byte> source)
    {
        fixed (byte* ptr = State)
        {
            Span<byte> target = new(ptr, 128);
            target.Clear();
            source.CopyTo(target);
        }
    }

    public unsafe fixed byte Details[128];

    public unsafe void SetDetails(ReadOnlySpan<byte> source)
    {
        fixed (byte* ptr = Details)
        {
            Span<byte> target = new(ptr, 128);
            target.Clear();
            source.CopyTo(target);
        }
    }

    public ActivityTimestamps Timestamps;

    public ActivityAssets Assets;

    public ActivityParty Party;

    public ActivitySecrets Secrets;

    public bool Instance;

    public uint SupportedPlatforms;
}