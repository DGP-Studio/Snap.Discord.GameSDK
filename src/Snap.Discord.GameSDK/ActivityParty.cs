namespace Snap.Discord.GameSDK;

public struct ActivityParty
{
    public unsafe fixed byte Id[128];

    public PartySize Size;

    public ActivityPartyPrivacy Privacy;
}