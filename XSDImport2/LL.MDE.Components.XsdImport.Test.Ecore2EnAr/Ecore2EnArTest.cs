using System;
using System.Collections.Generic;
using System.IO;
using LL.MDE.Components.Common.EnArLoader;
using LL.MDE.Components.Common.Util;
using LL.MDE.Components.XsdImport.Ecore2EnAr;
using LL.MDE.DataModels.EnAr;
using NMF.Interop.Ecore;
using NMF.Models.Repository;
using NUnit.Framework;

namespace LL.MDE.Components.XsdImport.Test.Ecore2EnAr
{
    [SetUpFixture]
    public class Ecore2EnArTestSetUp
    {
        public static EnArLoader Loader;


        [SetUp]
        public void Start()
        {
            Loader = new EnArLoader("output/project.eap");
        }

        [TearDown]
        public void End()
        {
            Loader.Close();
        }
    }

    public class Ecore2EnArTest
    {
        private static void GenericTest(string inputFile, string outputPackageId)
        {
            GenericTest(inputFile, null, outputPackageId);
        }

        private static void GenericTest(string inputFile, IDictionary<string, string> dependencies, string outputPackageId)
        {
            string projectFolder = VariousUtil.GetProjectFolder();
            string ecorePath = Path.Combine(projectFolder, inputFile);

            if (dependencies != null)
            {
                foreach (KeyValuePair<string, string> dependency in dependencies)
                {
                    // Load dependency using NMF, with a specific URI
                    string dependencyPath = Path.Combine(projectFolder, dependency.Value);
                    ModelRepository repository = (ModelRepository) EcoreInterop.Repository;
                    FileStream depFileStream = new FileInfo(dependencyPath).Open(FileMode.Open);
                    repository.Serializer.Deserialize(depFileStream,
                        new Uri(dependency.Key, UriKind.Absolute), repository, true);
                    depFileStream.Close();
                }
            }

            // Load ecore using NMF
            IEPackage package = EcoreInterop.LoadPackageFromFile(ecorePath);
            ISet<IEPackage> epackages = new HashSet<IEPackage>() {package};

            // Load output EnAr project
            Package outputContainerPackage = Ecore2EnArTestSetUp.Loader.GetEnAarPackage(outputPackageId);

            // Prepare transformation and start
            XsdImport.Ecore2EnAr.Ecore2EnAr importer = new XsdImport.Ecore2EnAr.Ecore2EnAr(outputContainerPackage, Ecore2EnArTestSetUp.Loader.Explorer);
            importer.ConstructMetamodel(epackages);
        }

        [Test]
        public void TestSimpleMetamodel()
        {
            GenericTest("input/simple.ecore", "{E65CD4CA-5656-4ea6-9D8A-FF9C94E5DC3F}");
        }


        [Test]
        public void TestGenericsMetamodel()
        {
            GenericTest("input/generics.ecore", "{C552BC71-7269-45d5-93A1-67EC8E1B976B}");
        }

        [Test]
        public void TestXmlTypeMetamodel()
        {
            GenericTest("input/XMLType.ecore", "{DD6588C1-7B94-417c-B481-72D3DF291D6A}");
        }

        [Test]
        public void TestCaexMetamodel()
        {
            Dictionary<string, string> dependencies = new Dictionary<string, string>()
            {
                {"http://www.eclipse.org/emf/2003/XMLType", "input/XMLType.ecore"}
            };
            GenericTest("input/caex.ecore", dependencies, "{05616D66-47DA-489b-B9E0-07692B1294FB}");
        }
    }
}