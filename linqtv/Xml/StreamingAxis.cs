using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace linqtv.Xml
{
    public class StreamingAxis
    {
        public static IEnumerable<XElement> AsEnumerable(Stream xmlStream, ISet<string> elementNames)
        {
            var normalizedNames = elementNames.Select(n => n.ToLower());

            using (var xmlReader = XmlReader.Create(xmlStream))
            {
                xmlReader.MoveToContent();

                while (xmlReader.Read())
                {
                    if ((XmlNodeType.Element == xmlReader.NodeType) && normalizedNames.Contains(xmlReader.Name))
                    {
                        var yieldElement = XNode.ReadFrom(xmlReader) as XElement;
                        if (null != yieldElement)
                            yield return yieldElement;
                    }
                }
                xmlReader.Close();
            }
        }
    }
}
