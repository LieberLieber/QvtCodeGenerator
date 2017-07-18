using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;

using NArrange.Core;
using NArrange.Core.CodeElements;
using NArrange.Core.Configuration;
using NArrange.CSharp;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.Utils
{
    public static class CodeFormatter
    {
        public static string Format(string cSharpCode)
        {
            // We prepare stuff
            CodeConfiguration configuration = CodeConfiguration.Default;
            configuration.Formatting.Tabs.TabStyle = TabStyle.Tabs;
            configuration.Formatting.LineSpacing.RemoveConsecutiveBlankLines = true;
            configuration.Formatting.Regions.Style = RegionStyle.NoDirective;
            CodeArranger codeArranger = new CodeArranger(configuration);
            ICodeElementParser parser = new CSharpParser { Configuration = configuration };

            // We parse
            StringReader reader = new StringReader(cSharpCode);
            ReadOnlyCollection<ICodeElement> elements = parser.Parse(reader);

            // We reorganize the AST
            elements = codeArranger.Arrange(elements);

            // We rewrite
            ICodeElementWriter codeWriter = new CSharpWriter { Configuration = configuration };
            StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);
            codeWriter.Write(elements, writer);
            return writer.ToString();
        }
    }
}