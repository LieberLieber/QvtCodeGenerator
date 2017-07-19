using System;

using LL.MDE.Components.Qvt.Common;
using LL.MDE.Components.Qvt.EnArInterface;
using LL.MDE.Components.Qvt.TestUtil;
using LL.MDE.Components.Qvt.Transformation.Demo1;
using LL.MDE.DataModels.EnAr;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.Demo1
{
    public class Demo1Test : BaseEnArTestClass
    {

        protected override string GetEnArFilePath()
        {
            return @"..\LL.MDE.Components.Qvt.Test\TestData\testModel.eap";
        }

        protected override void OtherInit()
        {
           }

        [Test]
        public void ExecuteDemo1()
        {
            Package packageA = GetLoader().GetEnAarPackage("{D272CF77-A976-444e-9405-E1B57AC5F460}");
            Package packageAOther = GetLoader().GetEnAarPackage("{E6B4988A-1848-46e3-B711-164D34F8E49B}");
            IMetaModelInterface editor = new EnArMetaModelInterface();
            Package outputContainer = GetLoader().GetEnAarPackage("{FF266277-B01C-45f3-83FD-08F6F992C17A}");
            int randomId = new Random().Next();
            Package output = (Package)outputContainer.Packages.AddNew("output" + randomId, "Package");
            outputContainer.Packages.Refresh();
            TransformationDemo1 transfo = new TransformationDemo1(editor);
            transfo.Relation1(packageA, "youpla", packageAOther, output);
        }
    }
}