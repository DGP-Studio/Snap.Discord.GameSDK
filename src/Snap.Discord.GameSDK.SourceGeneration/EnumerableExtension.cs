using System.Collections.Generic;

namespace Snap.Discord.GameSDK.SourceGeneration;

public static class EnumerableExtension
{
    public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, int count)
    {
        if (count == 0)
        {
            foreach(T? item in source)
            {
                yield return item;
            }

            yield break;
        }

        Queue<T> buffer = new(count + 1);

        foreach (T? item in source)
        {
            buffer.Enqueue(item);

            // 如果队列的大小超过了 count，就移除队列头部的元素
            if (buffer.Count > count)
            {
                yield return buffer.Dequeue();
            }
        }
    }
}