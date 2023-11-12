namespace Snap.Discord.GameSDK.Activity;

public partial struct ActivityParty
{
    public unsafe fixed byte Id[128];

    public PartySize Size;

    public ActivityPartyPrivacy Privacy;
}