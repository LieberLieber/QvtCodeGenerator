using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.TestUtil;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.Tests
{
    public class EnArImportQVTTests2 : BaseEnArTestWithQVTImporter
    {

        protected override string GetEnArFilePath()
        {
            return @"TestData/testModel2.eap";
        }

        private void BaseTest(string transformationName)
        {
            IRelationalTransformation result = importer.ConstructRelationalTransformation(transformationName);
            Assert.NotNull(result);
            Assert.IsNotEmpty(result.Rule, "The constructed QVT transformation has no Rule.");
        }


        [Test]
        public void TestEaToSimpleUML()
        {
            BaseTest("EaToSimpleUML");
        }

        [Test]
        public void TestUML2RDBMS()
        {
            BaseTest("umlToRdbms");
        }


    }
}