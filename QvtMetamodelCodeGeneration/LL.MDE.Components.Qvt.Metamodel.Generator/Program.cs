using System.CodeDom;

using Microsoft.CSharp;

using NMF.Interop.Ecore;
using NMF.Models.Meta;

namespace LL.MDE.Components.Qvt.Metamodel.Generator
{
    internal class Program
    {
        private static void GenerateCodeFor(string input, string output, string name)
        {
            EPackage package = EcoreInterop.LoadPackageFromFile(input);
            INamespace nMeta = EcoreInterop.Transform2Meta(package);
            CodeCompileUnit code = MetaFacade.CreateCode(nMeta, name);
            MetaFacade.GenerateCode(code, new CSharpCodeProvider(), output, true);
        }


        public static void Main(string[] args)
        {
            GenerateCodeFor(@"..\..\model\QVTRelation.ecore", @"..\..\..\..\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.Metamodel\Generated", "LL.MDE.Components.Qvt.Metamodel");
        }
    }
}
