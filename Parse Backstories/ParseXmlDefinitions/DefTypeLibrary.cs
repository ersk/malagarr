using ParseXmlDefinitions.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ParseXmlDefinitions.DefTypeLibrary;

namespace ParseXmlDefinitions
{
    public class DefTypeLibrary
    {
        public Dictionary<string, Dictionary<string, DefTypeProperty>> library;

        public DefTypeLibrary()
        {
            library = new();
        }

        public void ParseDefs(List<DefElement> defs)
        {
            foreach (DefElement def in defs)
            {

                if (library.ContainsKey(def.Name) == false)
                {
                    library.Add(def.Name, new());
                }

                library.TryGetValue(def.Name, out Dictionary<string, DefTypeProperty> list);

                foreach (BaseElement baseElement in def.Properties)
                {
                    DefTypeEnum defType = GetDefType(baseElement);

                    if (list.ContainsKey(baseElement.Name) == false)
                    {
                        DefTypeProperty defTypeProperty;

                        if (defType == DefTypeEnum.Complex)
                        {
                            defTypeProperty = ParseComplexElement((ComplexElement)baseElement);
                        }
                        else if (defType == DefTypeEnum.ListString)
                        {
                            defTypeProperty = ParseListElement((ListElement<string>)baseElement);
                        }
                        else if (defType == DefTypeEnum.ListString)
                        {
                            defTypeProperty = ParseListElement((ListElement<decimal>)baseElement);
                        }
                        else
                        {
                            defTypeProperty = new(defType, baseElement.Name);
                        }

                        list.Add(baseElement.Name, defTypeProperty);
                    }
                    else // property already exists
                    {
                        // but add new list values if needed
                        if (defType == DefTypeEnum.ListString)
                        {
                            list.TryGetValue(baseElement.Name, out DefTypeProperty defTypeProperty);
                            ((DefTypePropertyList<string>)defTypeProperty).values.UnionWith(((ListElement<string>)baseElement).Items);
                        }
                        else if (defType == DefTypeEnum.ListNumber)
                        {
                            list.TryGetValue(baseElement.Name, out DefTypeProperty defTypeProperty);
                            ((DefTypePropertyList<decimal>)defTypeProperty).values.UnionWith(((ListElement<decimal>)baseElement).Items);
                        }
                    }


                }
            }

        }
        public DefTypePropertyList<T> ParseListElement<T>(ListElement<T> listElement)
        {
            DefTypePropertyList<T> defList = new(listElement.Name);

            if (listElement.Items == null) throw new Exception("EMpty list.");

            defList.values = listElement.Items;

            return defList;
        }
        public DefTypePropertyComplex ParseComplexElement(ComplexElement complexElement)
        {
            DefTypePropertyComplex defTypeComplex = new(complexElement.Name);

            foreach (BaseElement element in complexElement.Properties)
            {

                if (defTypeComplex.properties.ContainsKey(element.Name) == false)
                {
                    DefTypeEnum defType = GetDefType(element);

                    DefTypeProperty defTypeProperty;

                    if (defType == DefTypeEnum.Complex)
                    {
                        defTypeProperty = ParseComplexElement((ComplexElement)element);
                    }
                    else
                    {
                        defTypeProperty = new(defType, element.Name);
                    }

                    defTypeComplex.properties.Add(element.Name, defTypeProperty);
                }
            }

            return defTypeComplex;
        }


        private DefTypeEnum GetDefType(BaseElement baseElement)
        {
            Type baseElementType = baseElement.GetType();


            if (baseElementType == typeof(BoolElement))
            {
                return DefTypeEnum.Boolean;
            }
            else if (baseElementType == typeof(StringElement))
            {
                return DefTypeEnum.String;
            }
            else if (baseElementType == typeof(NumberElement))
            {
                return DefTypeEnum.Number;
            }
            else if (baseElementType == typeof(ComplexElement))
            {
                return DefTypeEnum.Complex;
            }
            else if (baseElementType == typeof(ListElement<string>))
            {
                return DefTypeEnum.ListString;
            }
            else if (baseElementType == typeof(ListElement<decimal>))
            {
                return DefTypeEnum.ListNumber;
            }

            throw new Exception("Unrecognized element");
        }


        public enum DefTypeEnum
        {
            String,
            Number,
            Boolean,
            Complex,
            ListString,
            ListNumber
        }

        public class DefTypeProperty
        {
            public readonly DefTypeEnum DefType;
            public readonly string Name;

            public DefTypeProperty(DefTypeEnum DefType, string Name)
            {
                this.DefType = DefType;
                this.Name = Name;
            }
        }

        public class DefTypePropertyList<T> : DefTypeProperty
        {
            public HashSet<T> values;

            public DefTypePropertyList(string Name) : base(DefTypeEnum.Complex, Name)
            {
                values = new();
            }
        }

        public class DefTypePropertyComplex : DefTypeProperty
        {
            public Dictionary<string, DefTypeProperty> properties;

            public DefTypePropertyComplex(string Name) : base(DefTypeEnum.Complex, Name)
            {
                properties = new();
            }
        }
    }
}
