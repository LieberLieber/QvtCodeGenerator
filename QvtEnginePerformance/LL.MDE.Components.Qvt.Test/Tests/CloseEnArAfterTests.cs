using LL.MDE.Components.Qvt.TestUtil;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.Tests
{
    /// <summary>
    /// This class is used one per test session to initialize EnAr (at the beginning)
    /// and to exit EnAr (at the end).
    /// </summary>
   [SetUpFixture]
    public class CloseEnArAfterTests
    {
     
        [TearDown]
        public void RunAfterAnyTests()
        {
            EnArLoaderTestSingleton.GetInstance().Close();
        }
    }
}