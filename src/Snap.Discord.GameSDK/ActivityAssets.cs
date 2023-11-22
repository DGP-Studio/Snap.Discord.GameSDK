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
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = largeImage)
            {
                ByteString.Set(ptr, 128, value);
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
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = largeText)
            {
                ByteString.Set(ptr, 128, value);
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
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = smallImage)
            {
                ByteString.Set(ptr, 128, value);
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
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = smallText)
            {
                ByteString.Set(ptr, 128, value);
            }
        }
    }
}