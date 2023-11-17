namespace Snap.Discord.GameSDK;

public struct User
{
    /// <summary>
    /// the user's id
    /// </summary>
    public long Id;
    private unsafe fixed byte username[256];
    private unsafe fixed byte discriminator[8];
    private unsafe fixed byte avatar[128];

    /// <summary>
    /// if the user is a bot user
    /// </summary>
    public bool Bot;

    /// <summary>
    /// their name
    /// </summary>
    public unsafe string UserName
    {
        get
        {
            fixed (byte* ptr = username)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = username)
            {
                ByteString.Set(ptr, 256, value);
            }
        }
    }

    /// <summary>
    /// the user's unique discrim
    /// </summary>
    public unsafe string Discriminator
    {
        get
        {
            fixed (byte* ptr = discriminator)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = discriminator)
            {
                ByteString.Set(ptr, 8, value);
            }
        }
    }

    /// <summary>
    /// the hash of the user's avatar
    /// </summary>
    public unsafe string Avatar
    {
        get
        {
            fixed (byte* ptr = avatar)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = avatar)
            {
                ByteString.Set(ptr, 128, value);
            }
        }
    }
}