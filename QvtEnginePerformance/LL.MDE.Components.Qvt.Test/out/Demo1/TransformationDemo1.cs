namespace LL.MDE.Components.Qvt.Transformation.Demo1
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using LL.MDE.Components.Qvt.Common;
	using LL.MDE.DataModels.EnAr;

	public class TransformationDemo1 : GeneratedTransformation
	{
		public readonly RelationRelation1 RelationRelation1;

		internal readonly Dictionary<Tuple<string>, LL.MDE.DataModels.EnAr.Element> ElementKeys = new Dictionary<Tuple<string>, LL.MDE.DataModels.EnAr.Element>();
		internal readonly Dictionary<Tuple<string>, LL.MDE.DataModels.EnAr.Package> PackageKeys = new Dictionary<Tuple<string>, LL.MDE.DataModels.EnAr.Package>();

		private readonly IMetaModelInterface editor;

		public TransformationDemo1(IMetaModelInterface editor )
		{
			this.editor = editor;  this.RelationRelation1 = new RelationRelation1(editor, this);
		}

		public override void CallTopRelation(string topRelationName, List<object> parameters)
		{
			switch (topRelationName)
			{
							case "Relation1":
					Relation1((LL.MDE.DataModels.EnAr.Package)parameters[0],(string)parameters[1],(LL.MDE.DataModels.EnAr.Package)parameters[2],(LL.MDE.DataModels.EnAr.Package)parameters[3]);
					return;

			}
		}

		public void Relation1(LL.MDE.DataModels.EnAr.Package p,string someString,LL.MDE.DataModels.EnAr.Package p2,LL.MDE.DataModels.EnAr.Package po)
		{
			RelationRelation1.CheckAndEnforce(p,someString,p2,po) ;
		}
	}
}