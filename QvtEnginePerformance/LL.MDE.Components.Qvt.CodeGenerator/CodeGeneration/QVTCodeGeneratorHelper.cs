using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration.RelationTemplate;
using LL.MDE.Components.Qvt.QvtCodeGenerator.Utils;

using TransformationMainTemplate = LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration.TransformationTemplate.TransformationMainTemplate;

namespace LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration
{
    public static class QVTCodeGeneratorHelper
    {
        private static readonly IList<string> csharpKeywords = new List<string>()
        {
            "abstract",
            "as",
            "base",
            "bool",
            "break",
            "byte",
            "case",
            "catch",
            "char",
            "checked",
            "class",
            "const",
            "continue",
            "decimal",
            "default",
            "delegate",
            "do",
            "double",
            "else",
            "enum",
            "event",
            "explicit",
            "extern",
            "false",
            "finally",
            "fixed",
            "float",
            "for",
            "foreach",
            "goto",
            "if",
            "implicit",
            "in",
            "int",
            "interface",
            "internal",
            "is",
            "lock",
            "long",
            "namespace",
            "new",
            "null",
            "object",
            "operator",
            "out",
            "override",
            "params",
            "private",
            "protected",
            "public",
            "readonly",
            "ref",
            "return",
            "sbyte",
            "sealed",
            "short",
            "sizeof",
            "stackalloc",
            "static",
            "string",
            "struct",
            "switch",
            "this",
            "throw",
            "true",
            "try",
            "typeof",
            "uint",
            "ulong",
            "unchecked",
            "unsafe",
            "ushort",
            "using",
            "virtual",
            "void",
            "volatile",
            "while"
        };

        public static string GenerateCode(IRelation relation, bool useMetamodelInterface = true)
        {
            RelationMainTemplate template = new RelationMainTemplate(relation, useMetamodelInterface);
            string code = template.TransformText();

            // We prefix members accessed with keywords by @
            //TODO find better fix: protected keywords could also be used as domain names, hence as method args in C#
            foreach (string csharpKeyword in csharpKeywords)
            {
                code = code.Replace("." + csharpKeyword, ".@" + csharpKeyword);
            }

            return code;
        }

        public static string GenerateCode(IRelationalTransformation transformation, bool useMetamodelInterface)
        {
            TransformationMainTemplate template = new TransformationMainTemplate(transformation, useMetamodelInterface);
            string code = template.TransformText();
            return code;
        }

        public static string GenerateCodeFunctions(IRelationalTransformation transformation)
        {
            FunctionsInterfaceTemplate.FunctionsInterfaceTemplate template = new FunctionsInterfaceTemplate.FunctionsInterfaceTemplate(transformation);
            string code = template.TransformText();
            return code;
        }

        private static string PrepareOutputFolderString(string s)
        {
            if (s.Last() != '\\')
            {
                return s + '\\';
            }
            return s;
        }

        public static void GenerateCode(IRelationalTransformation transformation, string outputFolderAbsolute, bool useMetamodelInterface)
        {
            string code = GenerateCode(transformation, useMetamodelInterface);
            try
            {
                code = CodeFormatter.Format(code);
            }
            catch (Exception)
            {
                // TODO
            }
            if (!Directory.Exists(outputFolderAbsolute))
            {
                Directory.CreateDirectory(outputFolderAbsolute);
            }
            File.WriteAllText(PrepareOutputFolderString(outputFolderAbsolute) + QvtCodeGeneratorStrings.GetFileName(transformation), code);
        }

        public static void GenerateCodeFunctions(IRelationalTransformation transformation, string outputFolderAbsolute)
        {
            string code = GenerateCodeFunctions(transformation);
            try
            {
                code = CodeFormatter.Format(code);
            }
            catch (Exception)
            {
                // TODO
            }
            if (!Directory.Exists(outputFolderAbsolute))
            {
                Directory.CreateDirectory(outputFolderAbsolute);
            }
            File.WriteAllText(PrepareOutputFolderString(outputFolderAbsolute) + QvtCodeGeneratorStrings.GetFileNameFunctions(transformation), code);
        }

        public static void GenerateCode(IRelation relation, string outputFolderAbsolute, bool useMetamodelInterface = true)
        {
            string code = GenerateCode(relation, useMetamodelInterface);
            try
            {
                code = CodeFormatter.Format(code);
            }
            catch (Exception)
            {
                // TODO
            }
            File.WriteAllText(PrepareOutputFolderString(outputFolderAbsolute) + QvtCodeGeneratorStrings.GetFileName(relation), code);
        }

        public static void GenerateAllCode(IRelationalTransformation transformation, string outputFolderAbsolute, bool useMetamodelInterface = true)
        {
            GenerateCode(transformation, outputFolderAbsolute, useMetamodelInterface);
            if (transformation.OwnedOperation.Count > 0)
            {
                GenerateCodeFunctions(transformation, outputFolderAbsolute);
            }
            foreach (IRelation relation in transformation.Rule.OfType<IRelation>())
            {
                GenerateCode(relation, outputFolderAbsolute, useMetamodelInterface);
            }
        }
    }
}