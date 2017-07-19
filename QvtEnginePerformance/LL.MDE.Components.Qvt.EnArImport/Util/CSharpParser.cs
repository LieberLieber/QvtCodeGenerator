using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace LL.MDE.Components.Qvt.EnArImport.Util
{
    public static class CSharpParser
    {
        private const string BeginDummyClass = @"public class MyClass { public void MyMethod() {";
        private const string EndDummyClass = "}}";

        public static BlockSyntax ParseAnything(string anything)
        {
            string fakeClassCode = BeginDummyClass + anything + EndDummyClass;
            SyntaxTree fakeClassTree = CSharpSyntaxTree.ParseText(fakeClassCode);
            return fakeClassTree.GetRoot().ChildNodes().First().ChildNodes().First().ChildNodes().OfType<BlockSyntax>().Single();
        }

        public static List<StatementSyntax> ParseInstructions(string instructionsString)
        {
            BlockSyntax stuff = ParseAnything(instructionsString);
            return stuff.Statements.ToList();
        }

        public static ExpressionSyntax ParseExpression(string expressionString)
        {
            BlockSyntax stuff = ParseAnything(expressionString);
            return stuff.Statements.OfType<ExpressionStatementSyntax>().Single().Expression;
        }

        private class IdentifierWalker : CSharpSyntaxWalker
        {
            public readonly ISet<string> Result = new HashSet<string>();

            /// <summary>Called when the visitor visits a IdentifierNameSyntax node.</summary>
            public override void VisitIdentifierName(IdentifierNameSyntax node)
            {
                if (!(node.Parent is InvocationExpressionSyntax))
                    Result.Add(node.Identifier.Text);
            }
        }

        public static ISet<string> ExtractNonMethodIdentifiersFromExpression(ExpressionSyntax expressionSyntax)
        {
            IdentifierWalker walker = new IdentifierWalker();
            walker.Visit(expressionSyntax);
            return walker.Result;
        }
    }

}