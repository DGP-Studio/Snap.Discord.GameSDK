namespace Snap.Discord.GameSDK.Playground;

internal class Program
{
    static async Task Main()
    {
        Discord discord = new(1173950861647552623L, CreateFlags.NoRequireDiscord);

        ThreadPool.QueueUserWorkItem(obj =>
        {
            Discord d = (Discord)obj!;
            while (true)
            {
                d.RunCallbacks();
                Thread.Sleep(100);
            }
        }, discord);

        ActivityManager activityManager = discord.GetActivityManager();

        Activity activity = default;
        activity.State = "服务器：天空岛";
        activity.Details = "在提瓦特大陆中探索";
        activity.Timestamps.Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        activity.Assets.LargeImage = "icon";
        activity.Assets.LargeText = "原神";
        activity.Assets.SmallImage = "hutaoicon2";
        activity.Assets.SmallText = "使用 Snap Hutao 启动";
        Result result = await activityManager.UpdateActivityAsync(activity);
        Console.WriteLine($"UpdateActivity Result: {result}");
        Console.ReadLine();
    }
}