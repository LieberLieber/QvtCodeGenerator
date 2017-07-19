using System;
using System.Diagnostics;

using LL.MDE.Components.Common.EnArLoader;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.TestUtil
{
    public abstract class BaseEnArTestClass
    {
        private Stopwatch stopWatch;
        private long initTime;
        private long testTime;
        private long otherInitTime;


        protected EnArLoader GetLoader()
        {
            return EnArLoaderTestSingleton.GetInstance().GetLoader(GetEnArFilePath());
        }

         [SetUp]
        public void SetUp()
        {
            stopWatch = new Stopwatch();

            // Init EnAr and measure time
            stopWatch.Start();
            GetLoader(); // Indirectly performs the EA initialisation
            stopWatch.Stop();
            initTime = stopWatch.ElapsedMilliseconds;
            stopWatch.Reset();

            // Other init
            stopWatch.Start();
             OtherInit();
            stopWatch.Stop();
            otherInitTime = stopWatch.ElapsedMilliseconds;
            stopWatch.Reset();

            // Start stopwatch for the upcoming test
            stopWatch.Start();
        }

        private static void DisplayTime(string label, long time)
        {
            Console.WriteLine(label + ": " + time + "ms");
        }

        [TearDown]
        public void TearDown()
        {
            // Stop stopwatch for the finished test
            stopWatch.Stop();
            testTime = stopWatch.ElapsedMilliseconds;
            stopWatch.Reset();

            // Display times
            Console.WriteLine();
            Console.WriteLine("================= Time results ===================");
            DisplayTime("initTime", initTime);
            DisplayTime("otherInit", otherInitTime);
            DisplayTime("testTime", testTime);
            Console.WriteLine("==================================================");
        }

        protected abstract string GetEnArFilePath();

        protected abstract void OtherInit();
    }
}