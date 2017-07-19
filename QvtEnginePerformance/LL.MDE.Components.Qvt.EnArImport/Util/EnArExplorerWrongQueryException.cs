using System;
using System.Runtime.Serialization;

namespace LL.MDE.Components.Qvt.EnArImport
{
    [Serializable]
    internal class EnArExplorerWrongQueryException : Exception
    {
        public EnArExplorerWrongQueryException(string m) : base(m)
        {
        }
    }
}