namespace Snap.Discord.GameSDK;

public struct Relationship
{
    /// <summary>
    /// what kind of relationship it is
    /// </summary>
    public RelationshipType Type;

    /// <summary>
    /// the user the relationship is for
    /// </summary>
    public User User;

    /// <summary>
    /// that user's current presence
    /// </summary>
    public Presence Presence;
}