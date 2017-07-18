using System.Collections.Generic;

using LL.MDE.Components.Qvt.Metamodel.QVTTemplate;

namespace LL.MDE.Components.Qvt.Metamodel.CustomExtensions.QVTTemplateExtensions
{
	public static class ObjectTemplateExpExtensions
	{
		private static readonly
			Dictionary<IObjectTemplateExp, bool> allValues = new Dictionary<IObjectTemplateExp, bool>();

		public static void SetAntiTemplate(this IObjectTemplateExp objectTemplateExp, bool value)
		{
			allValues[objectTemplateExp] = value;
		}

		public static bool IsAntiTemplate(this IObjectTemplateExp objectTemplateExp)
		{
			return allValues.ContainsKey(objectTemplateExp) && allValues[objectTemplateExp];
		}
	}
}