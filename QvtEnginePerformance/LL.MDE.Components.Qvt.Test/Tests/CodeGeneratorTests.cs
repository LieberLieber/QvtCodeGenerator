using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration;
using LL.MDE.Components.Qvt.TestUtil;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.Tests
{
    public class CodeGeneratorTests : BaseEnArTestWithQVTImporter
    {
        protected override string GetEnArFilePath()
        {
            return @"TestData/testModel.eap";
        }

     
        private void BaseTest(string transformationName, bool useMetamodelInterface)
        {
            IRelationalTransformation transfo = importer.ConstructRelationalTransformation(transformationName);
            string outputFolder = GetLoader().AbsolutePathToOutput + "/" + transformationName;
            QVTCodeGeneratorHelper.GenerateAllCode(transfo, outputFolder, true);
        }

        [Test]
        public void TestDemo1()
        {
            BaseTest("Demo1", true);
        }

        [Test]
        public void TestEA2FMEA1()
        {
            BaseTest("EA2FMEA", false);
        }

    
    }
}