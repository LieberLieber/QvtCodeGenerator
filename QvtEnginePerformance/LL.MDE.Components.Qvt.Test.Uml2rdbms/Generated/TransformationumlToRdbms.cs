namespace LL.MDE.Components.Qvt.Transformation.umlToRdbms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.SimpleRDBMS;
	using LL.MDE.DataModels.SimpleUML;

	public class TransformationumlToRdbms : GeneratedTransformation
	{
		public readonly IFunctions Functions;
		public readonly RelationAssocToFKey RelationAssocToFKey;
		public readonly RelationAttributeToColumn RelationAttributeToColumn;
		public readonly RelationClassToTable RelationClassToTable;
		public readonly RelationComplexAttributeToColumn RelationComplexAttributeToColumn;
		public readonly RelationPackageToSchema RelationPackageToSchema;
		public readonly RelationPrimitiveAttributeToColumn RelationPrimitiveAttributeToColumn;
		public readonly RelationSuperAttributeToColumn RelationSuperAttributeToColumn;

		private readonly IMetaModelInterface editor;

		public TransformationumlToRdbms(IMetaModelInterface editor , IFunctions Functions)
		{
			this.editor = editor;  this.RelationPackageToSchema = new RelationPackageToSchema(editor, this);
			this.Functions = Functions;  this.RelationClassToTable = new RelationClassToTable(editor, this);
			this.Functions = Functions;  this.RelationPrimitiveAttributeToColumn = new RelationPrimitiveAttributeToColumn(editor, this);
			this.Functions = Functions;  this.RelationComplexAttributeToColumn = new RelationComplexAttributeToColumn(editor, this);
			this.Functions = Functions;  this.RelationSuperAttributeToColumn = new RelationSuperAttributeToColumn(editor, this);
			this.Functions = Functions;  this.RelationAssocToFKey = new RelationAssocToFKey(editor, this);
			this.Functions = Functions;  this.RelationAttributeToColumn = new RelationAttributeToColumn(editor, this);
			this.Functions = Functions;
		}

		public override void CallTopRelation(string topRelationName, List<object> parameters)
		{
			switch (topRelationName)
			{
							case "PackageToSchema":
					PackageToSchema((LL.MDE.DataModels.SimpleUML.Package)parameters[0],(LL.MDE.DataModels.SimpleRDBMS.Schema)parameters[1]);
					return;

			}
		}

		public void PackageToSchema(LL.MDE.DataModels.SimpleUML.Package p,LL.MDE.DataModels.SimpleRDBMS.Schema s)
		{
			RelationPackageToSchema.CheckAndEnforce(p,s) ;
		}
	}
}