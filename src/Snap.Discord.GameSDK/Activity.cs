namespace Snap.Discord.GameSDK;

public struct Activity
{
    public ActivityType Type;

    /// <summary>
    /// your application id - this is a read-only field
    /// </summary>
    public readonly long ApplicationId;
    private unsafe fixed byte name[128];
    private unsafe fixed byte state[128];
    private unsafe fixed byte details[128];

    /// <summary>
    /// helps create elapsed/remaining timestamps on a player's profile
    /// </summary>
    public ActivityTimestamps Timestamps;

    /// <summary>
    /// assets to display on the player's profile
    /// </summary>
    public ActivityAssets Assets;

    /// <summary>
    /// information about the player's party
    /// </summary>
    public ActivityParty Party;

    /// <summary>
    /// secret passwords for joining and spectating the player's game
    /// </summary>
    public ActivitySecrets Secrets;

    /// <summary>
    /// whether this activity is an instanced context, like a match
    /// </summary>
    public bool Instance;
    public uint SupportedPlatforms;

    /// <summary>
    /// name of the application - this is a read-only field
    /// </summary>
    public unsafe string Name
    {
        get
        {
            fixed (byte* ptr = name)
            {
                return ByteString.Get(ptr);
            }
        }
    }

    /// <summary>
    /// the player's current party status
    /// </summary>
    public unsafe string State
    {
        get
        {
            fixed (byte* ptr = state)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = state)
            {
                ByteString.Set(ptr, 128, value);
            }
        }
    }

    /// <summary>
    /// what the player is currently doing
    /// </summary>
    public unsafe string Details
    {
        get
        {
            fixed (byte* ptr = details)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = details)
            {
                ByteString.Set(ptr, 128, value);
            }
        }
    }
}