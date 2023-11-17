using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;

namespace Snap.Discord.GameSDK;

public struct ActivityAssets
{
    private unsafe fixed byte largeImage[128];
    private unsafe fixed byte largeText[128];
    private unsafe fixed byte smallImage[128];
    private unsafe fixed byte smallText[128];

    /// <summary>
    /// see <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-asset-image">Activity Asset Image</see>
    /// </summary>
    public unsafe string LargeImage
    {
        get
        {
            fixed (byte* ptr = largeImage)
            {
                return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr));
            }
        }
        set
        {
            fixed (byte* ptr = largeImage)
            {
                Span<byte> target = new(ptr, 128);
                target.Clear();
                Utf8.FromUtf16(value, target, out _, out _);
            }
        }
    }

    /// <summary>
    /// hover text for the large image
    /// </summary>
    public unsafe string LargeText
    {
        get
        {
            fixed (byte* ptr = largeText)
            {
                return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr));
            }
        }
        set
        {
            fixed (byte* ptr = largeText)
            {
                Span<byte> target = new(ptr, 128);
                target.Clear();
                Utf8.FromUtf16(value, target, out _, out _);
            }
        }
    }

    /// <summary>
    /// see <see href="https://discord.com/developers/docs/topics/gateway-events#activity-object-activity-asset-image">Activity Asset Image</see>
    /// </summary>
    public unsafe string SmallImage
    {
        get
        {
            fixed (byte* ptr = smallImage)
            {
                return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr));
            }
        }
        set
        {
            fixed (byte* ptr = smallImage)
            {
                Span<byte> target = new(ptr, 128);
                target.Clear();
                Utf8.FromUtf16(value, target, out _, out _);
            }
        }
    }

    /// <summary>
    /// hover text for the small image
    /// </summary>
    public unsafe string SmallText
    {
        get
        {
            fixed (byte* ptr = smallText)
            {
                return Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(ptr));
            }
        }
        set
        {
            fixed (byte* ptr = smallText)
            {
                Span<byte> target = new(ptr, 128);
                target.Clear();
                Utf8.FromUtf16(value, target, out _, out _);
            }
        }
    }
}