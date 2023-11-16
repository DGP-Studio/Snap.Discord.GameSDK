using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Snap.Discord.GameSDK.Playground;

internal class Program
{
    static ManualResetEventSlim eventSlim = new();

    static unsafe void Main()
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
        activity.Type = ActivityType.Playing;
        activity.ApplicationId = 1173950861647552623L;
        //activity.SetName("Genshin Impact"u8);
        activity.SetState("服务器：天空岛"u8);
        activity.SetDetails("在提瓦特大陆中探索"u8);
        activity.Timestamps.Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        activity.Assets.SetLargeImage("icon"u8);
        activity.Assets.SetLargeText("原神"u8);
        activity.Assets.SetSmallImage("hutaoicon2"u8);
        activity.Assets.SetSmallText("使用 Snap Hutao 启动"u8);
        activityManager.UpdateActivity(activity, UpdateActivityHandler.Create(&OnUpdateActivity));
        eventSlim.Wait();
        _ = 1;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    static void OnUpdateActivity(Result result)
    {
        Console.WriteLine($"UpdateActivity: {result}");
        eventSlim.Set();
    }
}