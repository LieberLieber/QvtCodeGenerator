using System;
using System.Collections.Generic;
using System.IO;
using EA;
using LL.MDE.Components.Common.EnArLoader;
using LL.MDE.Components.Common.Util;
using NUnit.Framework;

namespace SandboxEADebug
{
  
    public class Class1
    {
        [Test]
        public void Test()
        {
            // Load ecore using NMF
            string path = @"C:\Users\ebousse\Downloads\test-templates.eap";
            var loader = new EnArLoader(path, true);
            var package = loader.GetEnAarPackage("{6E831E0A-E6EC-4633-B6FC-9D0669EA9074}");
            Console.WriteLine("Yay!");
        }

    }
}
