namespace FManager.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static partial class Utils
    {
        public static void ForEachAsync<T>(this IEnumerable<T> data, Func<T, Task> func)
        {
            var tasks = data.Select(item => Task.Run(() => func(item)));

            Task.WaitAll(tasks.ToArray());
        }

        public static void ForEach<T>(this IEnumerable<T> data, Action<T> act)
        {
            foreach (var item in data)
            {
                act(item);
            }
        }
    }
}