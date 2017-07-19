namespace LL.MDE.Components.Qvt.Transformation.EA2FMEA
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;
	using LL.MDE.DataModels.XML;

	public class TransformationEA2FMEA : GeneratedTransformation
	{
		private readonly IMetaModelInterface editor;

		public TransformationEA2FMEA(IMetaModelInterface editor)
		{
			this.editor = editor;
		}

		public override void CallTopRelation(string topRelationName, List<object> parameters)
		{
			switch (topRelationName)
			{
							case "EA2FMEA_Start":
					EA2FMEA_Start((LL.MDE.DataModels.XML.XMLFile)parameters[0],(LL.MDE.DataModels.EnAr.Package)parameters[1]);
					return;

			}
		}

		public void EA2FMEA_Start(LL.MDE.DataModels.XML.XMLFile fmeaFile,LL.MDE.DataModels.EnAr.Package alP)
		{
			new RelationEA2FMEA_Start(editor, this).CheckAndEnforce(fmeaFile,alP) ;
		}
	}
}