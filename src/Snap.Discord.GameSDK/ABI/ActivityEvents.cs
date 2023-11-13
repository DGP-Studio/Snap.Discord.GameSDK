namespace Snap.Discord.GameSDK.ABI;

public struct ActivityEvents
{
    internal ActivityJoinHandler OnActivityJoin;
    internal ActivitySpectateHandler OnActivitySpectate;
    internal ActivityJoinRequestHandler OnActivityJoinRequest;
    internal ActivityInviteHandler OnActivityInvite;
}