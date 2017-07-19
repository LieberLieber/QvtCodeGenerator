using System.Collections.Generic;

namespace LL.MDE.DataModels.XML
{
    public class Tag
    {
        public string tagname { get; set; }

        public string value { get; set; }

        public List<Tag> childTags { get; set; } = new List<Tag>();

        public Tag nextSibbling { get; set; }

        public Tag childTag { get; set; }

        public List<Attribute> attributes { get; set; } = new List<Attribute>();

        #region Overrides of Object

        public override string ToString()
        {
            string result = "<" + tagname + " ";
            if (attributes != null)
                result += string.Join(" ", attributes);

            result += ">" + value + "\r\n";
            if (childTags != null)
                result += string.Join(" ", childTags);

            result += "\r\n</" + tagname + ">";

            if (nextSibbling != null)
            {
                result += "\r\n" + nextSibbling;
            }

            return result;
        }

        #endregion
    }
}