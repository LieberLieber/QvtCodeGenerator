using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using NMF.Utilities;

namespace LL.MDE.Components.Qvt.EnArImport.Util
{
    public class QvtKeyParserResult
    {
        public string ClassName;
        public readonly IList<IList<string>> NavigatedProperties = new List<IList<string>>();
    }

    public static class QvtKeyParser
    {
        public static IList<QvtKeyParserResult> Parse(string s)
        {
            IList<QvtKeyParserResult> result = new List<QvtKeyParserResult>();
            List<StatementSyntax> parsed = CSharpParser.ParseInstructions(s);
            foreach (ExpressionStatementSyntax statementSyntax in parsed.OfType<ExpressionStatementSyntax>())
            {
                QvtKeyParserResult qvtKeyParserResult = new QvtKeyParserResult();
                result.Add(qvtKeyParserResult);
                InvocationExpressionSyntax invocationExpressionSyntax = statementSyntax.Expression as InvocationExpressionSyntax;
                if (invocationExpressionSyntax != null)
                {
                    IdentifierNameSyntax caller = invocationExpressionSyntax.Expression as IdentifierNameSyntax;
                    if (caller != null)
                    {
                        qvtKeyParserResult.ClassName = caller.ToString();
                    }

                    foreach (ArgumentSyntax argument in invocationExpressionSyntax.ArgumentList.Arguments)
                    {
                        IList<string> path = new List<string>();
                        ExpressionSyntax argumentContent = argument.Expression;
                        if (argumentContent is IdentifierNameSyntax)
                        {
                            path.Add(argumentContent.ToString());
                        }
                        else if (argumentContent is MemberAccessExpressionSyntax)
                        {
                            IList<string> properties = ManageMemberAccess((MemberAccessExpressionSyntax)argumentContent);
                            path.AddRange(properties);
                        }
                        qvtKeyParserResult.NavigatedProperties.Add(path);
                    }
                }
            }
            return result;
        }

        private static IList<string> ManageMemberAccess(MemberAccessExpressionSyntax argumentContent)
        {
            IList<string> result = new List<string>();
            ExpressionSyntax left = argumentContent.Expression;
            if (left is MemberAccessExpressionSyntax)
            {
                result.AddRange(ManageMemberAccess((MemberAccessExpressionSyntax)left));
            }
            else if (left is IdentifierNameSyntax)
            {
                result.Add(left.ToString());
            }
            result.Add(argumentContent.Name.ToString());
            return result;
        }
    }
}