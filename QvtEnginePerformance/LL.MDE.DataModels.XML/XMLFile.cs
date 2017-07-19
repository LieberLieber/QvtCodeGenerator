namespace LL.MDE.DataModels.XML
{
	public class XMLFile
	{
		public string filename { get; set; }

		public string encoding { get; set; }

		public Tag content { get; set; }

		#region Overrides of Object


		public override string ToString()
		{
			return content.ToString();
		}

		#endregion
	}
}
