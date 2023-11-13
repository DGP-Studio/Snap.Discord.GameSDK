namespace Snap.Discord.GameSDK;

public struct UserAchievement
{
    public long UserId;

    public long AchievementId;

    public byte PercentComplete;

    public unsafe fixed byte UnlockedAt[64];
}