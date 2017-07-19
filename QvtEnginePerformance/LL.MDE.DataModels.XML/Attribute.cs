namespace LL.MDE.DataModels.XML
{
	public class Attribute
	{
		public string name { get; set; }

		public string value { get; set; }

		#region Overrides of Object

	
		public override string ToString()
		{
			return name + "='" + value + "'";
		}

		#endregion
	}
}
