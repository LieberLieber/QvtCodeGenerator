using System;
using System.Collections.Generic;

using LL.MDE.Components.Qvt.Metamodel.EssentialOCL;

namespace LL.MDE.Components.Qvt.Metamodel.EssentialOCL
{
	public class CSharpOpaqueExpression : OclExpression
	{
		public string Code;
		public ISet<IVariable> BindsTo = new HashSet<IVariable>();
	}
}