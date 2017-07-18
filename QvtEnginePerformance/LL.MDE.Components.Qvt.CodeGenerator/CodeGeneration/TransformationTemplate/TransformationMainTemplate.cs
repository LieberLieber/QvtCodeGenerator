﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration.TransformationTemplate
{
    using LL.MDE.Components.Qvt.Common;
    using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.EMOFExtensions;
    using LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTRelationExtensions;
    using LL.MDE.Components.Qvt.Metamodel.EMOF;
    using LL.MDE.Components.Qvt.Metamodel.EMOFExtensions;
    using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;
    using LL.MDE.Components.Qvt.Metamodel.QVTBase;
    using LL.MDE.Components.Qvt.Metamodel.QVTRelation;
    using LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration;
    using LL.MDE.Components.Qvt.QvtCodeGenerator.CodeGeneration.RelationTemplate;
    using NMF.Utilities;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class TransformationMainTemplate : TransformationMainTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("\r\n");
            this.Write("\r\n");
            this.Write("\r\n\r\n\r\n");
            this.Write("\r\n\r\nusing System;\r\nusing System.Collections.Generic; \r\nusing System.Linq;\r\nusing " +
                    "LL.MDE.Components.Qvt.Common;\r\n");
            
            #line 23 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"

    // Small hack to bypass limitation of ForTea plugin ( https://github.com/MrJul/ForTea/issues/3 )
    // We redeclare members so that ForTea finds them, and enables code completion etc. 
    // The errors can be ignored, as the generated .cs file compiles correctly.
    IRelationalTransformation transformation = this.Transformation;
    ISet<ITypedModel> validEnforceTargetParams = this.validEnforceTargetParams;
    bool useMetamodelInterface = this.useMetamodelInterface;
    
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 32 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
 // Generating of the "usings", for each package of each metamodel used in the transformation
    foreach (IPackage package in transformation.ModelParameter.Select(p => p.UsedPackage).SelectMany(i => i).Distinct())
    { 
            
            #line default
            #line hidden
            this.Write("\r\nusing ");
            
            #line 36 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(package.Name));
            
            #line default
            #line hidden
            this.Write(";\r\n\r\n");
            
            #line 38 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n");
            
            #line 40 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
 // Generation of the namespace 
            
            #line default
            #line hidden
            
            #line 41 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.Namespace(transformation)));
            
            #line default
            #line hidden
            this.Write(" \r\n{ \r\n\r\n\r\n");
            
            #line 45 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
 // Generation of the Transfo class 
            
            #line default
            #line hidden
            this.Write("public class ");
            
            #line 46 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.TransformationName(transformation)));
            
            #line default
            #line hidden
            this.Write(" : GeneratedTransformation {\r\n\r\n\r\n \tprivate readonly ");
            
            #line 49 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nameof(IMetaModelInterface)));
            
            #line default
            #line hidden
            this.Write(" editor;");
            
            #line 49 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"


    bool hasFunctions = !transformation.OwnedOperation.IsNullOrEmpty();
	// Functions object, if any functions are defined
    if (hasFunctions)
    {
         
            
            #line default
            #line hidden
            this.Write("public readonly ");
            
            #line 55 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.FunctionsInterfaceName(transformation)));
            
            #line default
            #line hidden
            this.Write(" Functions;");
            
            #line 55 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"

    }

	// Storing each relation instance once
    foreach (IRelation relation in transformation.Rule.OfType<IRelation>())
    {
        
            
            #line default
            #line hidden
            this.Write(" public readonly ");
            
            #line 61 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.RelationClassName(relation)));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 61 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.RelationClassName(relation)));
            
            #line default
            #line hidden
            this.Write("; ");
            
            #line 61 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"

    }

	// Dictionnaries for keys
    foreach (IKey key in transformation.OwnedKey)
    {
        IList<IProperty> allKeyProperties = new List<IProperty>();
        allKeyProperties.AddRange(key.Part);
        allKeyProperties.AddRange(key.PropertyPaths().Select(pp => pp.Properties.Last()));
        string tupleTypes = string.Join(",", allKeyProperties.Select(p => p.Type.GetRealTypeName()));
        
            
            #line default
            #line hidden
            this.Write("internal readonly Dictionary<Tuple<");
            
            #line 71 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(tupleTypes));
            
            #line default
            #line hidden
            this.Write(">, ");
            
            #line 71 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(key.Identifies.GetRealTypeName()));
            
            #line default
            #line hidden
            this.Write("> ");
            
            #line 71 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.KeyDictionnaryName(key)));
            
            #line default
            #line hidden
            this.Write(" = new Dictionary<Tuple<");
            
            #line 71 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(tupleTypes));
            
            #line default
            #line hidden
            this.Write(">, ");
            
            #line 71 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(key.Identifies.GetRealTypeName()));
            
            #line default
            #line hidden
            this.Write(">();");
            
            #line 71 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"

    }

	// Constructor
		 
            
            #line default
            #line hidden
            this.Write("public ");
            
            #line 75 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.TransformationName(transformation)));
            
            #line default
            #line hidden
            this.Write(" (");
            
            #line 75 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(nameof(IMetaModelInterface)));
            
            #line default
            #line hidden
            this.Write(" editor ");
            
            #line 75 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(hasFunctions ? ", "+ QvtCodeGeneratorStrings.FunctionsInterfaceName(transformation) +" Functions" : ""));
            
            #line default
            #line hidden
            this.Write(") {\r\n\r\n\t\t\tthis.editor = editor;");
            
            #line 77 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
 
			 foreach (IRelation relation in transformation.Rule.OfType<IRelation>())
    {
        
            
            #line default
            #line hidden
            this.Write("  this.");
            
            #line 80 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.RelationClassName(relation)));
            
            #line default
            #line hidden
            this.Write(" = new ");
            
            #line 80 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.RelationClassName(relation)));
            
            #line default
            #line hidden
            this.Write("(editor, this); \r\n\t\t");
            
            #line 81 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"

		if (hasFunctions)
		 {
         
            
            #line default
            #line hidden
            this.Write("this.Functions = Functions;");
            
            #line 84 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"

		}
    }
		
            
            #line default
            #line hidden
            this.Write("}");
            
            #line 87 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
 
		 


foreach (IRelation relation in transformation.Rule.OfType<IRelation>().Where(r => r.IsTopLevel.GetValueOrDefault(false)))
{
    
            
            #line default
            #line hidden
            this.Write("public void ");
            
            #line 93 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write(" (");
            
            #line 93 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(RelationTemplateHelper.GenerateRelationParams(true, relation)));
            
            #line default
            #line hidden
            this.Write(") {\r\n\t\t");
            
            #line 94 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(QvtCodeGeneratorStrings.RelationClassName(relation)));
            
            #line default
            #line hidden
            this.Write(".CheckAndEnforce(");
            
            #line 94 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(RelationTemplateHelper.GenerateRelationParams(false, relation)));
            
            #line default
            #line hidden
            this.Write(") ;\r\n\t}");
            
            #line 95 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"

}

 

            
            #line default
            #line hidden
            this.Write("public override void CallTopRelation (string topRelationName, List<object> parame" +
                    "ters)\r\n\t{\r\n\t\tswitch (topRelationName)\r\n\t\t{\r\n\t\t\t");
            
            #line 103 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
 foreach (IRelation relation in transformation.Rule.OfType<IRelation>().Where(r => r.IsTopLevel.GetValueOrDefault(false)))
{ 
            
            #line default
            #line hidden
            this.Write("\t\t\t\tcase \"");
            
            #line 105 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write("\":\r\n\t\t\t\t\t");
            
            #line 106 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(relation.Name));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 106 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(string.Join(",", relation.Domain.OfType<IRelationDomain>().Select(
    d => "(" + d.RootVariable.Type.GetRealTypeName() + ")parameters[" + relation.Domain.IndexOf(d) + "]"))));
            
            #line default
            #line hidden
            this.Write(");\r\n\t\t\t\t\treturn;\r\n\t\t\t");
            
            #line 109 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationMainTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\r\n\t\t\t}\r\n\t\t}\r\n\r\n\t} // end class\r\n} // end namespace\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 12 "C:\Users\ebousse\Source\Repos\ChristianDopplerLabors\Software\QvtEnginePerformance\trunk\src\LL.MDE.Components.Qvt.CodeGenerator\CodeGeneration\TransformationTemplate\TransformationHelperTemplate.tt"


	public void GenerateAttributesAndConstructor(IRelationalTransformation transformation)
	{
	     


		
	}




        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class TransformationMainTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
