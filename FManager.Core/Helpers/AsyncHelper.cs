using System;
using System.Threading;
using System.Threading.Tasks;

namespace FManager.Core.Helpers
{
    /// <summary>
    /// This class provide safety way to run an async method in a sync way.
    /// https://gist.github.com/ChrisMcKee/6664438
    /// </summary>
    public static partial class AsyncHelper
    {
        /// <summary>
		///     Execute's an async Task<T> method which has a void return value synchronously
		/// </summary>
		/// <param name="task">
		///     Task<T> method to execute
		/// </param>
		public static void RunSync(Func<Task> task)
        {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            synch.Post(async _ =>
            {
                try
                {
                    await task();
                }
                catch (Exception e)
                {
                    synch.InnerException = e;
                    throw;
                }
                finally
                {
                    synch.EndMessageLoop();
                }
            }, null);
            synch.BeginMessageLoop();

            SynchronizationContext.SetSynchronizationContext(oldContext);
        }

        public static void RunSync(this Task task)
        {
            RunSync(() => task);
        }

        /// <summary>
        ///     Execute's an async Task<T> method which has a T return type synchronously
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="task">
        ///     Task<T> method to execute
        /// </param>
        /// <returns></returns>
        public static T RunSync<T>(Func<Task<T>> task)
        {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            var ret = default(T);
            synch.Post(async _ =>
            {
                try
                {
                    ret = await task();
                }
                catch (Exception e)
                {
                    synch.InnerException = e;
                    throw;
                }
                finally
                {
                    synch.EndMessageLoop();
                }
            }, null);
            synch.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(oldContext);
            return ret;
        }

        public static T RunSync<T>(this Task<T> task)
        {
            return RunSync(() => task);
        }
    }
}
