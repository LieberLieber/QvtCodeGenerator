using System;

using LL.MDE.Components.Qvt.Common;
using LL.MDE.Components.Qvt.TestUtil;
using LL.MDE.Components.Qvt.Transformation.EA2FMEA;
using LL.MDE.DataModels.EnAr;
using LL.MDE.DataModels.XML;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.EA2FMEATest
{
    public class EA2FMEATest : BaseEnArTestClass
    {
        protected override string GetEnArFilePath()
        {
            return @"..\LL.MDE.Components.Qvt.Test\TestData\testModel.eap";
        }

        [Test]
        public void TestSimplestModel()
        {
            Package input = Loader.GetEnAarPackage("{C0315CAE-0D96-4b33-B553-0D9827E4DD6C}");
            IMetaModelInterface editor = new ReflectiveMetamodelInterface();
            XMLFile output = new XMLFile();
            TransformationEA2FMEA transfo = new TransformationEA2FMEA(editor);
            transfo.EA2FMEA_Start(output, input);
            Console.WriteLine(output.ToString());
        }
    }
}