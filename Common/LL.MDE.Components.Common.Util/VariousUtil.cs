using System;
using System.IO;

namespace LL.MDE.Components.Common.Util
{
    public class VariousUtil
    {
        /// <summary>
        /// Operation to retrieve the Visual studio project folder of the executed program/test.
        /// </summary>
        /// <returns></returns>
        public static string GetProjectFolder()
        {
            // Find the model test file in the VS project
            string binDebugDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Directory.GetParent(Directory.GetParent(binDebugDirectory).FullName).FullName; // we do bin/Debug/../.. to reach the project root
        }
    }
}