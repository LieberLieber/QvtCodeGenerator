using System;
using System.Runtime.Serialization;

using LL.MDE.DataModels.EnAr;

namespace LL.MDE.Components.Qvt.EnArImport
{
    [Serializable]
    internal class InvalidQVTRelationsModelException : Exception
    {
        public object enArObject { get; }

        public InvalidQVTRelationsModelException(string message) :
            base(message)
        {
        }

        public InvalidQVTRelationsModelException(string message, Element enArObject) :
            base(message + " | Element: " + enArObject)
        {
            this.enArObject = enArObject;
        }

        public InvalidQVTRelationsModelException(string message, Connector connector)
            : base(message + " | Element: " + connector)
        {
            enArObject = connector;
        }
    }
}