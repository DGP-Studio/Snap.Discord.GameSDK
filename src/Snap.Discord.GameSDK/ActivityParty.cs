namespace Snap.Discord.GameSDK;

public struct ActivityParty
{
    private unsafe fixed byte id[128];

    /// <summary>
    /// info about the size of the party
    /// </summary>
    public PartySize Size;
    public ActivityPartyPrivacy Privacy;

    /// <summary>
    /// a unique identifier for this party
    /// </summary>
    public unsafe string Id
    {
        get
        {
            fixed (byte* ptr = id)
            {
                return ByteString.Get(ptr);
            }
        }
        set
        {
            fixed (byte* ptr = id)
            {
                ByteString.Set(ptr, 128, value);
            }
        }
    }
}