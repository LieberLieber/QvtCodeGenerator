using System;
using System.IO;
using System.Threading;

using EA;
using LL.MDE.DataAccess.EnAr.Hybrid;
using LL.MDE.Components.Common.Util;

using NUnit.Framework;

using File = System.IO.File;
using Package = LL.MDE.DataModels.EnAr.Package;

namespace LL.MDE.Components.Common.EnArLoader
{
    /// <summary>
    /// To manage the retrieval of all the test data from a chosen EA file.
    /// </summary>
    public class EnArLoader
    {
        private const string GeneratorOutputPath = @"out\";

        public string AbsolutePathToOutput { get; }
        public EnArExplorer Explorer { get; }

        public readonly RepositoryImpl currentLlRepository;
        private readonly string projectFolder;
        private bool dataModelReadyToUse;

        /// <summary>
        /// To initialize EA with a given project file.
        /// The "Close" method should be called when it is over.
        /// </summary>
        /// <param name="fileName"></param>
        public EnArLoader(string fileName, bool isAbsolute = false, bool makeCopy = true)
        {
            if (AbsolutePathToOutput == null)
            {
                // Find the model test file in the VS project
                projectFolder = VariousUtil.GetProjectFolder();
                // Create output folder
                AbsolutePathToOutput = Path.Combine(projectFolder, GeneratorOutputPath);
            }
            if (currentLlRepository == null)
            {
                // Creates EA instance
                Repository currentEaRepository = new Repository();

                // Opens the model file in the EA instance
                string absolutePathToModel = isAbsolute ? fileName : Path.Combine(projectFolder, fileName); // and from there we can find the "models" folder

                if (makeCopy)
                {
                    string absolutePathToModelCopy = absolutePathToModel;
                    absolutePathToModelCopy = Path.ChangeExtension(absolutePathToModelCopy, ".tmp.eap");
                    if (File.Exists(absolutePathToModelCopy))
                        File.Delete(absolutePathToModelCopy);
                    File.Copy(absolutePathToModel, absolutePathToModelCopy);
                    absolutePathToModel = absolutePathToModelCopy;
                }

                bool openResult = currentEaRepository.OpenFile(absolutePathToModel);
                Assert.True(openResult, "The file " + absolutePathToModel + "could not be opened");

                // Opens the model    
                currentLlRepository = new RepositoryImpl(currentEaRepository);
                currentLlRepository.ChachingFinished += HybridRepositoryCachingFinished;
                for (int i = 0; i < 50; i++)
                {
                    if (dataModelReadyToUse == false)
                        Thread.Sleep(500);
                }

                if (dataModelReadyToUse == false)
                    throw new Exception("Timeout when trying to open EnAr file " + fileName);

                Explorer = new EnArExplorer(currentLlRepository, currentEaRepository);
            }
        }

        ~EnArLoader()
        {
            Close();
        }

        private void HybridRepositoryCachingFinished(object sender, EventArgs e)
        {
            dataModelReadyToUse = true;
        }

        /// <summary>
        /// Closes the current EA instance.
        /// </summary>
        public void Close()
        {
            try
            {
                currentLlRepository?.CloseFile();
                currentLlRepository?.Exit();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Looks for a Package with a specific GUID in the current EnAr instance.
        /// (can only be called after Init)
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Package GetEnAarPackage(string guid)
        {
            return currentLlRepository.GetPackageByGuid(guid);
        }
        
    }
}