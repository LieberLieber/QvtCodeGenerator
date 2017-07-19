using System;
using System.Collections.Generic;

using LL.MDE.Components.Qvt.EnArImport.Util;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using NUnit.Framework;

namespace LL.MDE.Components.Qvt.Test.Tests
{

    public class CSharpParserTests
    {
        private class PrinterWalker : CSharpSyntaxWalker
        {
            private static int tabs;

            public override void Visit(SyntaxNode node)
            {
                tabs++;
                string indents = new string('\t', tabs);
                Console.WriteLine(indents + node.Kind() + "[" + node + "]");
                base.Visit(node);
                tabs--;
            }
        }

        private static void PrintNodeContent(SyntaxNode node)
        {
            PrinterWalker walker = new PrinterWalker();
            walker.Visit(node);
        }

        [Test]
        public void TestInstructions()
        {
            List<StatementSyntax> result = CSharpParser.ParseInstructions(@"name = elName + ':' + classifierName; CreateDecompositionLink(structureId, parentSeDecomposition); BlockProperty2StructureElement(childEl, fmSeDecomposition, structureElements); ");
            foreach (StatementSyntax statementSyntax in result)
            {
                PrintNodeContent(statementSyntax);
            }
        }

        [Test]
        public void TestString()
        {
            ExpressionSyntax result = CSharpParser.ParseExpression("\"12\"");
            PrintNodeContent(result);
        }

        [Test]
        public void TestComplexString()
        {
            ExpressionSyntax result = CSharpParser.ParseExpression("\"12\" + someVar");
            PrintNodeContent(result);
        }

        [Test]
        public void TestInt()
        {
            ExpressionSyntax result = CSharpParser.ParseExpression("12");
            PrintNodeContent(result);
        }

        [Test]
        public void TestQvtKey()
        {
            List<StatementSyntax> result = CSharpParser.ParseInstructions(" Package(Name, Element.Stereotype); Element(Name); Other(One.Two.Three)");
            foreach (StatementSyntax statementSyntax in result)
            {
                PrintNodeContent(statementSyntax);
            }
        }
    }
}
