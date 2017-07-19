using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.TestUtil;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.Tests
{
    public class EnArImportQVTTests : BaseEnArTestWithQVTImporter
    {

        protected override string GetEnArFilePath()
        {
            return @"TestData/testModel.eap";
        }

        private void BaseTest(string transformationName)
        {
            IRelationalTransformation result = importer.ConstructRelationalTransformation(transformationName);
            Assert.NotNull(result);
            Assert.IsNotEmpty(result.Rule, "The constructed QVT transformation has no Rule.");
        }



        [Test]
        public void TestDemo1()
        {
            BaseTest("Demo1");
        }

        [Test]
        public void TestEA2FMEA1()
        {
            BaseTest("EA2FMEA");
        }




    }
}