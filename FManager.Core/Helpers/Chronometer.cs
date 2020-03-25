namespace FManager.Core.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public class Chronometer
    {
        private static Chronometer instance;

        private Stopwatch stopWatch;

        private Chronometer()
        {
        }

        public static Chronometer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Chronometer();
                }

                return instance;
            }
        }

        public void Start(string initMsg)
        {
            this.stopWatch = new Stopwatch();
            Debug.WriteLine(initMsg);
            this.stopWatch.Start();
        }

        public void Finish(string endMsg)
        {
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Debug.WriteLine(endMsg + $" -> Runtime: { elapsedTime}");
        }
    }
}
