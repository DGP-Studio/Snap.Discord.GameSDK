namespace Snap.Discord.GameSDK;

public enum RelationshipType
{
    /// <summary>
    /// user has no intrinsic relationship
    /// </summary>
    None,

    /// <summary>
    /// user is a friend
    /// </summary>
    Friend,

    /// <summary>
    /// user is blocked
    /// </summary>
    Blocked,

    /// <summary>
    /// user has a pending incoming friend request to connected user
    /// </summary>
    PendingIncoming,

    /// <summary>
    /// current user has a pending outgoing friend request to user
    /// </summary>
    PendingOutgoing,

    /// <summary>
    /// user is not friends, but interacts with current user often (frequency + recency)
    /// </summary>
    Implicit,
}