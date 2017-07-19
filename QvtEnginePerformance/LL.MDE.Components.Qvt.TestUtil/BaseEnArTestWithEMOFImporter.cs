using LL.MDE.Components.Qvt.EnArImport;

namespace LL.MDE.Components.Qvt.TestUtil
{
    public abstract class BaseEnArTestWithEMOFImporter : BaseEnArTestClass
    {

        protected EnArImporterEMOF importer;

        protected override void OtherInit()
        {
            importer = new EnArImporterEMOF(GetLoader().Explorer);
        }
    }
}