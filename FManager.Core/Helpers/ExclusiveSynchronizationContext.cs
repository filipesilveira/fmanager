using System;
using System.Collections.Generic;
using System.Threading;

namespace FManager.Core.Helpers
{
    /// <summary>
    /// https://gist.github.com/ChrisMcKee/6664438
    /// </summary>
    public static partial class AsyncHelper
    {
        private class ExclusiveSynchronizationContext : SynchronizationContext
        {
            private readonly Queue<Tuple<SendOrPostCallback, object>> items =
                new Queue<Tuple<SendOrPostCallback, object>>();

            private readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);

            private bool done;
            public Exception InnerException { get; set; }

            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException("We cannot send to our same thread");
            }

            public override void Post(SendOrPostCallback d, object state)
            {
                lock (this.items)
                {
                    this.items.Enqueue(Tuple.Create(d, state));
                }
                this.workItemsWaiting.Set();
            }

            public void EndMessageLoop()
            {
                this.Post(_ => this.done = true, null);
            }

            public void BeginMessageLoop()
            {
                while (!this.done)
                {
                    Tuple<SendOrPostCallback, object> task = null;
                    lock (this.items)
                    {
                        if (this.items.Count > 0)
                        {
                            task = this.items.Dequeue();
                        }
                    }
                    if (task != null)
                    {
                        task.Item1(task.Item2);
                        if (this.InnerException != null) // the method threw an exeption
                        {
                            throw new AggregateException("AsyncHelpers.Run method threw an exception.", this.InnerException);
                        }
                    }
                    else
                    {
                        this.workItemsWaiting.WaitOne();
                    }
                }
            }

            public override SynchronizationContext CreateCopy()
            {
                return this;
            }
        }
    }
}
