using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Linqtv
{
    internal static class StreamingAxis
    {
        //dont forget that XML is case sensitive
        public static IEnumerable<XElement> AsEnumerable(Stream xmlStream, ISet<string> elementNames)
        {
            using (var xmlReader = XmlReader.Create(xmlStream))
            {
                xmlReader.MoveToContent();

                while (xmlReader.Read())
                {
                    if ((XmlNodeType.Element != xmlReader.NodeType) || !elementNames.Contains(xmlReader.Name)) continue;
                    var yieldElement = XNode.ReadFrom(xmlReader) as XElement;
                    if (null != yieldElement)
                        yield return yieldElement;
                }
            }
        }
    }
}