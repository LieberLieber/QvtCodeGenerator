using LL.MDE.Components.Qvt.EnArImport;

namespace LL.MDE.Components.Qvt.TestUtil
{
    public abstract class BaseEnArTestWithQVTImporter : BaseEnArTestClass

{
        protected EnArImporterQVT importer;



        protected override void OtherInit()
        {
            importer = new EnArImporterQVT(GetLoader().Explorer);
        }
    }
}