using System;

namespace Snap.Discord.GameSDK;

public struct ActivityAssets
{
    public unsafe fixed byte LargeImage[128];

    public unsafe void SetLargeImage(ReadOnlySpan<byte> source)
    {
        fixed (byte* ptr = LargeImage)
        {
            Span<byte> target = new(ptr, 128);
            target.Clear();
            source.CopyTo(target);
        }
    }

    public unsafe fixed byte LargeText[128];

    public unsafe void SetLargeText(ReadOnlySpan<byte> source)
    {
        fixed (byte* ptr = LargeText)
        {
            Span<byte> target = new(ptr, 128);
            target.Clear();
            source.CopyTo(target);
        }
    }

    public unsafe fixed byte SmallImage[128];

    public unsafe void SetSmallImage(ReadOnlySpan<byte> source)
    {
        fixed (byte* ptr = SmallImage)
        {
            Span<byte> target = new(ptr, 128);
            target.Clear();
            source.CopyTo(target);
        }
    }

    public unsafe fixed byte SmallText[128];

    public unsafe void SetSmallText(ReadOnlySpan<byte> source)
    {
        fixed (byte* ptr = SmallText)
        {
            Span<byte> target = new(ptr, 128);
            target.Clear();
            source.CopyTo(target);
        }
    }
}