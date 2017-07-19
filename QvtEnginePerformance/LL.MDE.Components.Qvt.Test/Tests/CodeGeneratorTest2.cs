using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration;
using LL.MDE.Components.Qvt.TestUtil;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.Tests
{
    public class CodeGeneratorTests2 : BaseEnArTestWithQVTImporter
    {
        protected override string GetEnArFilePath()
        {
            return @"TestData/testModel2.eap";
        }

     
        private void BaseTest(string transformationName, bool useMetamodelInterface)
        {
            IRelationalTransformation transfo = importer.ConstructRelationalTransformation(transformationName);
            string outputFolder = GetLoader().AbsolutePathToOutput + "/" + transformationName;
            QVTCodeGeneratorHelper.GenerateAllCode(transfo, outputFolder, true);
        }

        [Test]
        public void TestEaToSimpleUML()
        {
            BaseTest("EaToSimpleUML", true);
        }

        [Test]
        public void TestUML2RDBMS()
        {
            BaseTest("umlToRdbms", true);
        }
    }
}