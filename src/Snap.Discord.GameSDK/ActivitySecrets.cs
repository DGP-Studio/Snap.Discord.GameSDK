namespace Snap.Discord.GameSDK;

public struct ActivitySecrets
{
    private unsafe fixed byte match[128];
    private unsafe fixed byte join[128];
    private unsafe fixed byte spectate[128];

    /// <summary>
    /// unique hash for the given match context
    /// </summary>
    public unsafe string Match
    {
        get
        {
            fixed (byte* ptr = match)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = match)
            {
                ByteString.Set(ptr, 128, value);
            }
        }
    }

    /// <summary>
    /// unique hash for chat invites and Ask to Join
    /// </summary>
    public unsafe string Join
    {
        get
        {
            fixed (byte* ptr = join)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = join)
            {
                ByteString.Set(ptr, 128, value);
            }
        }
    }

    /// <summary>
    /// unique hash for Spectate button
    /// </summary>
    public unsafe string Spectate
    {
        get
        {
            fixed (byte* ptr = spectate)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = spectate)
            {
                ByteString.Set(ptr, 128, value);
            }
        }
    }
}