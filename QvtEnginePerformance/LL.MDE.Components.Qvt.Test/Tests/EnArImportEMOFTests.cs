using System;

using LL.MDE.Components.Qvt.Metamodel.EMOF;
using LL.MDE.Components.Qvt.TestUtil;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.Tests
{
    public class EnArImportEMOFTests : BaseEnArTestWithEMOFImporter
    {


        protected override string GetEnArFilePath()
        {
            return @"TestData/testModel.eap";
        }

        private void BaseTest(string metamodelName)
        {
            Tuple<IPackage, string> res = importer.ConstructMetamodel(metamodelName);
            Assert.NotNull(res.Item1, "The constructed EMOF package is null");
            Assert.True(res.Item1.NestedPackage.Count > 0 || res.Item1.OwnedType.Count > 0, "The constructed EMOF package is empty");

        }


        [Test]
        public void TestEnAr()
        {
            BaseTest("LL.MDE.DataModels.EnAr");
        }
        [Test]
        public void TestSimpleUML()
        {
            BaseTest("SimpleUML");
        }

        [Test]
        public void TestXML()
        {
            BaseTest("LL.MDE.DataModels.XML");
        }

  

    }
}