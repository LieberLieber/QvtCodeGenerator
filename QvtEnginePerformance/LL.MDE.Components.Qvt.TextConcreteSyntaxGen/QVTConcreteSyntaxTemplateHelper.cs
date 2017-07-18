using System;
using System.Collections.Generic;
using System.Linq;

using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.EMOFExtensions;
using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
using LL.MDE.Components.Qvt.Metamodel.QVTBase;
using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
using LL.MDE.Components.Qvt.Metamodel.QVTTemplate;

namespace LL.MDE.Components.Qvt.TextConcreteSyntaxGen
{
    public partial class QVTConcreteSyntaxTemplate
    {
        private readonly IRelationalTransformation transformation;
        private const string Indent = "\t";

        public QVTConcreteSyntaxTemplate(IRelationalTransformation transformation)
        {
            this.transformation = transformation;
        }

        private void GenerateString(IOclExpression exp)
        {
            if (exp is IObjectTemplateExp)
                GenerateString((IObjectTemplateExp)exp);
            else if (exp is IVariableExp)
                GenerateString((IVariableExp)exp);
            else if (exp is CSharpOpaqueExpression)
                GenerateString((CSharpOpaqueExpression)exp);
            else if (exp is Assignment)
                GenerateString((Assignment)exp);
            else if (exp is IRelationCallExp)
                GenerateString((IRelationCallExp)exp);
            else
                throw new Exception("OCL Expression not managed: "+exp);
        }

        private void GenerateString(Assignment assignment)
        {
            Write("#Assignment[" + assignment.AssignedVariable.Name + " = ");
            GenerateString(assignment.Value);
            Write("]");
        }

        private void GenerateString(IRelationCallExp relationCall)
        {
            Write("#RelationCallExp[" + relationCall.ReferredRelation.Name + "(");
            foreach (IOclExpression arg in relationCall.Argument)
            {
                GenerateString(arg);
                if (relationCall.Argument.Last() != arg) 
                    Write(", ");
            }
            Write(")]");
        }

        public void GenerateString(IPattern where)
        {
            foreach (IPredicate predicate in where.Predicate)
            {
                GenerateString(predicate.ConditionExpression);
                WriteLine(";");
            }
        }

        private void GenerateString(CSharpOpaqueExpression exp, bool manylines = false)
        {
            List<string> variablesNames = exp.BindsTo.Select(a => a.Name).ToList();
            string variablesNamesList = string.Join(",", variablesNames);
            if (manylines)
            {
                WriteLine("#CSharpOpaqueExpression[");
                PushIndent();
                string[] instructions = exp.Code.Split(';');
                foreach (string instruction in instructions.Where(i => !string.IsNullOrWhiteSpace(i)))
                {
                    string toWrite = instruction.Trim() + ";";
                    if (instructions.Last() == instruction)
                        Write(toWrite);
                    else
                        WriteLine(toWrite);
                }
                PopIndent();
                Write("]");
            }
            else
            {
                Write("#CSharpOpaqueExpression[");
                Write(exp.Code + "]");
            }

            if (variablesNames.Count > 0)
                Write("[variables: " + variablesNamesList + "]");

            if (manylines)
            {
                WriteLine("");
            }
        }

        private void GenerateString(IVariableExp exp)
        {
            Write(exp.ReferredVariable.Name);
        }

        private void PushIndent()
        {
            PushIndent(Indent);
        }

        private void GenerateString(IObjectTemplateExp template, bool start = false)
        {
            if (template.BindsTo != null)
            {
                Write(template.BindsTo.Name + ":" + template.BindsTo.Type.GetRealTypeName());
                WriteLine(" {");
                PushIndent();
                foreach (IPropertyTemplateItem prop in template.Part)
                {
                    Write(prop.ReferredProperty.Name + " = ");
                    GenerateString(prop.Value);
                    if (template.Part.Last() != prop)
                        WriteLine(",");
                }
                PopIndent();
                WriteLine(start ? "};" : "}");
            }
        }

        public static string GetFileName(IRelationalTransformation transformation)
        {
            return "Transformation" + transformation.Name + ".qvt";
        }
    }
}