namespace Snap.Discord.GameSDK.Achievement;

public struct UserAchievement
{
    public long UserId;

    public long AchievementId;

    public byte PercentComplete;

    public unsafe fixed byte UnlockedAt[64];
}