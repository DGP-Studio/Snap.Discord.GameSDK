namespace Snap.Discord.GameSDK.ABI;

internal struct ActivityEvents
{
    internal ActivityJoinHandler OnActivityJoin;
    internal ActivitySpectateHandler OnActivitySpectate;
    internal ActivityJoinRequestHandler OnActivityJoinRequest;
    internal ActivityInviteHandler OnActivityInvite;
}