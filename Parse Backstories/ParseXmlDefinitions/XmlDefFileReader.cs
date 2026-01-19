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

        public List<DefElement> Parse()
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

            List<DefElement>  defs = TryReadDefs(root);

            //TryWriteChildren(root);

            //then output it
            //Console.WriteLine(root.OuterXml);

            return defs;
        }

        private List<DefElement> TryReadDefs(XmlNode defsNode)
        {
            if(defsNode.ChildNodes.Count == 0)
            {
                throw new Exception("Defs node contained no defs.");
            }

            List<DefElement> defs = new();

            foreach (XmlNode defNode in defsNode.ChildNodes)
            {
                defs.Add(TryReadDef(defNode));
            }

            return defs;
        }

        private DefElement TryReadDef(XmlNode defNode)
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

            List<BaseElement> refProperties = new List<BaseElement>();
            AddDefProperties(defNode, ref refProperties);

            defEle.Properties = refProperties;

            return defEle;
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

                BaseElement childProperty = null;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                XmlNode firstChild = propertyNode.FirstChild;
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                if (firstChild.NodeType == XmlNodeType.Text)
                {
                    childProperty = ParseTextElement(propertyNode);
                }
                else if (firstChild.NodeType == XmlNodeType.Element)
                {
                    childProperty = ParseElement(propertyNode);
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

  

        private BaseElement ParseElement(XmlNode propertyNode)
        {
            XmlNode? firstChild = propertyNode.FirstChild;

            if (firstChild == null) throw new Exception("firstChild was null");

            if (firstChild.Name == "li")
            {
                // List
                return ParseListElement(propertyNode);
            }
            else
            {
                // Complex
                return ParseComplexElement(propertyNode);
            }
        }

        private BaseElement ParseComplexElement(XmlNode propertyNode)
        {
            XmlNode? firstChild = propertyNode.FirstChild;
            if (firstChild == null) throw new Exception("firstChild was null");

            List<BaseElement> refProperties = new List<BaseElement>();
            AddDefProperties(propertyNode, ref refProperties);

            ComplexElement complexElement = new ComplexElement(propertyNode.Name.Trim());
            complexElement.Properties = refProperties;

            return complexElement;
        }

        private BaseElement ParseListElement(XmlNode propertyNode)
        {
            XmlNode? firstChild = propertyNode.FirstChild;
            if (firstChild == null) throw new Exception("firstChild was null");

            // list
            XmlNode? firstNode = firstChild.ChildNodes.Item(0);

            if (firstChild.FirstChild.NodeType == XmlNodeType.Text)
            {

            }
            else
            {
                string s = "";
            }


            if (firstNode == null) throw new Exception("List was empty");
            Type listType;

            BaseElement? listElement = null;

            switch (GetNodeTextType(firstNode, out object nodeCastValue))
            {
                case TextNodeSubTypeEnum.Bool:

                    throw new Exception("List of bools makes no sense");

                case TextNodeSubTypeEnum.Number:


                    ListElement<decimal> numberListElement = new ListElement<decimal>(propertyNode.Name.Trim());
                    AddListItems(firstChild, ref numberListElement);
                    listElement = numberListElement;
                    break;

                case TextNodeSubTypeEnum.String:

                    ListElement<string> stringListElement = new ListElement<string>(propertyNode.Name.Trim());
                    AddListItems(firstChild, ref stringListElement);
                    listElement = stringListElement;
                    break;

                default: throw new Exception("Could not compute type of first list item.");
            }

            if (listElement == null)
            {
                throw new Exception("listELement was null.");
            }

            return listElement;
        }
        private void AddListItems<T>(XmlNode firstChild, ref ListElement<T> listElementRef)
        {
            foreach (XmlNode listItemNode in firstChild.ChildNodes)
            {
                if (listItemNode.InnerText != null)
                {
                    listElementRef.Items!.Add((T)Convert.ChangeType(listItemNode.InnerText, typeof(T)));
                }
            }
        }

        private BaseElement ParseTextElement(XmlNode propertyNode)
        {
            XmlNode firstChild = propertyNode.FirstChild;

            switch (GetNodeTextType(firstChild, out object nodeCastValue))
            {
                case TextNodeSubTypeEnum.Bool:
                    return new BoolElement(propertyNode.Name.Trim(), (bool)nodeCastValue);
                    break;
                case TextNodeSubTypeEnum.Number:
                    return new NumberElement(propertyNode.Name.Trim(), (decimal)nodeCastValue);

                case TextNodeSubTypeEnum.String:
                    return new StringElement(propertyNode.Name.Trim(), (string)nodeCastValue);
            }

            throw new Exception("Failed to parse text element.");
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
