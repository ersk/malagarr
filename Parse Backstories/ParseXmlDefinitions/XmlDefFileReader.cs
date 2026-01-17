using System.Xml;
using System.Xml.Linq;
using System.Linq;
using ParseXmlDefinitions.Model;

namespace ParseXmlDefinitions
{
    public class XmlDefFileReader : IDisposable
    {
        string filePath;
        FileStream fs;
        XmlDocument xmlDoc;

        public XmlDefFileReader(string filePath)
        {

            this.filePath = filePath;

            fs = File.OpenRead(filePath);

            xmlDoc = new XmlDocument();
            xmlDoc.Load(fs);
        }

        public void Parse()
        {

            //get the first node
            XmlNode? root = xmlDoc.FirstChild;

            if (root == null)
            {
                throw new Exception("No root element.");
            }

            // Ignore xml declaration
            if (root.Name == "xml")
            {
                root = root.NextSibling;
            }

            if (root == null)
            {
                throw new Exception("No root element.");
            }

            if (root.Name != "Defs")
            {

                throw new Exception("Root node should be named 'Defs'. Instead got '" + root.Name + "'.");
            }

            Console.WriteLine(root.Name);

            TryReadDefs(root);

            //TryWriteChildren(root);

            //then output it
            //Console.WriteLine(root.OuterXml);
        }

        private void TryReadDefs(XmlNode defsNode)
        {
            if(defsNode.ChildNodes.Count == 0)
            {
                throw new Exception("Defs node contained no defs.");
            }

            foreach (XmlNode defNode in defsNode.ChildNodes)
            {
                TryReadDef(defNode);
            }
        }

        private void TryReadDef(XmlNode defNode)
        {
            Console.WriteLine($"Read {defNode.Name}...");

            //look for defName property

            XmlNode? defNameNode = defNode.SelectSingleNode("defName");

            if(defNameNode == null)
            {
                throw new Exception("Def node does not have a defName child node.");
            }

            if (string.IsNullOrWhiteSpace(defNameNode.Name))
            {
                throw new Exception("Def name node name was null or whitespace.");
            }
            DefElement defEle = new(defNode.Name, defNameNode.Name);

            defEle.Properties = new();


        }

        private void AddDefProperties(XmlNode defNode, ref List<BaseElement> properties)
        {
            foreach (XmlNode propertyNode in defNode.ChildNodes)
            {
                // We have already parsed the defName - so skip this property
                if(propertyNode.Name == "defName")
                {
                    continue;
                }

                if (propertyNode.HasChildNodes == false)
                {
                    // ignore empty element?
                    continue;
                }

                BaseElement childProperty;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                XmlNode firstChild = propertyNode.FirstChild;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                if (firstChild.NodeType == XmlNodeType.Text)
                {
                    switch(GetNodeTextType(propertyNode, out object nodeCastValue))
                    {
                        case TextNodeSubTypeEnum.Bool:
                            childProperty = new BoolElement(propertyNode.Name.Trim(), (bool)nodeCastValue);
                            break;
                        case TextNodeSubTypeEnum.Number:
                            childProperty = new NumberElement(propertyNode.Name.Trim(), (decimal)nodeCastValue);
                            break;
                        case TextNodeSubTypeEnum.String:
                            childProperty = new StringElement(propertyNode.Name.Trim(), (string)nodeCastValue);
                            break;
                    }
                }
                else if (firstChild.NodeType == XmlNodeType.Element)
                {
                    if(firstChild.Name == "li")
                    {
                        // list

                        foreach(XmlNode listItemNode in firstChild.ChildNodes)
                        {
                            if(listItemNode.InnerText != null)
                            {
                                listItemNode.
                            }
                        }
                    }
                    else
                    {
                        // complex
                    }
                }
                else
                {
                    throw new Exception("Unexpected format when trying to get properties.");
                }


                // Add property to parent
                if(properties == null) properties = new List<BaseElement>();
                properties.Add(childProperty);
            }
        }

        private TextNodeSubTypeEnum GetNodeTextType(XmlNode xmlNode, out object outCastValue)
        {
            if(xmlNode.NodeType != XmlNodeType.Text)
            {
                throw new Exception($"Expected text node; got {xmlNode.NodeType}.");
            }

            if (string.IsNullOrWhiteSpace(xmlNode.Value))
            {
                throw new Exception("Expected text node with a value. Value was null or whitespace.");
            }

            string elValueTrimmed = xmlNode.Value.Trim();
            string elValueTrimmedLower = elValueTrimmed.ToLower();

            if (elValueTrimmedLower == "true" || elValueTrimmedLower == "false")
            {
                outCastValue = Convert.ToBoolean(elValueTrimmedLower);
                return TextNodeSubTypeEnum.Bool;
            }
            if(Decimal.TryParse(elValueTrimmed, out decimal tryParseResult))
            {
                outCastValue = tryParseResult;
                return TextNodeSubTypeEnum.Number;
            }
            outCastValue = elValueTrimmed;
            return TextNodeSubTypeEnum.String;

        }
        //public void TryWriteChildren(XmlNode node)
        //{

        //    foreach (XmlNode childNode in node.ChildNodes)
        //    {
        //        Console.WriteLine($"{childNode.Name}");
        //        TryWriteChildren(childNode);
        //    }
        //}

        public void Dispose()
        {
            fs.Dispose();
        }
    }


}
