using EA;

using LL.MDE.Components.Common.EnArLoader;
using LL.MDE.DataAccess.EnAr.Hybrid;
using LL.MDE.Components.Qvt.EnArImport;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration;

namespace LL.MDE.Components.Qvt.EnArIntegration
{
    public class EnArIntegrationHelper
    {
        /// <summary>
        /// Generates the code for a transformation element of a given EnAr instance.
        /// This method can be called from EnAr UI directly.
        /// </summary>
        /// <param name="eaRepository">The EnAr instance.</param>
        /// <param name="transformationGuid">The identifier of the transformation for code generation.</param>
        /// <param name="absoluteOutputFolder">The output folder to put the code into.</param>
        /// <param name="useMetamodelInterface">If true, the generated code will rely on an IMetamodelInterface object. Otherwise, it will rely on standard C# getters/setters.</param>
        public static void GenerateTransformationCode(Repository eaRepository, string transformationGuid, string absoluteOutputFolder, bool useMetamodelInterface = true)
        {
            // Create hybrid repository of an EA instance
            RepositoryImpl hybridrepo = new RepositoryImpl(eaRepository);

            // Import the transformation as real qvt model
            EnArExplorer explorer = new EnArExplorer(hybridrepo, eaRepository);
            EnArImporterQVT importer = new EnArImporterQVT(explorer);
            IRelationalTransformation relationalTransformation = importer.ConstructRelationalTransformationFromGuid(transformationGuid);

            // Generate code from qvt model
            QVTCodeGeneratorHelper.GenerateAllCode(relationalTransformation, absoluteOutputFolder, useMetamodelInterface);
        }
    }
}