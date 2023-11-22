using Snap.Discord.GameSDK.ABI;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Snap.Discord.GameSDK.Playground;

internal class Program
{
    static async Task Main()
    {
        using (Discord discord = new(1173950861647552623L, CreateFlags.NoRequireDiscord))
        {
            SetLogHook(discord);

            ThreadPool.QueueUserWorkItem(obj =>
            {
                Discord d = (Discord)obj!;
                while (true)
                {
                    Thread.Sleep(100);
                    d.RunCallbacks();
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

    private static unsafe void SetLogHook(Discord discord)
    {
        discord.SetLogHook(LogLevel.Debug, SetLogHookHandler.Create(&LogMessage));
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
    private static unsafe void LogMessage(LogLevel level, byte* message)
    {
        string msg = Encoding.UTF8.GetString(MemoryMarshal.CreateReadOnlySpanFromNullTerminated(message));
        Console.WriteLine($"[{level}] {msg}");
    }
}