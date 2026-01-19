using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseXmlDefinitions.Model
{

    public enum TextNodeSubTypeEnum
    {
        String,
        Number,
        Bool
    }

    public abstract class BaseElement
    {

        public string Name { get; private set; }

        public BaseElement(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Element name was null or whitespace.");
            }
            Name = name;
        }
    }

    public class StringElement : BaseElement
    {
        public string Value { get; private set; }

        public StringElement(string name, string value)
            : base(name)
        {
            Value = value;
        }
    }
    public class NumberElement : BaseElement
    {
        public decimal Value { get; private set; }

        public NumberElement(string name, decimal value)
            : base(name)
        {
            Value = value;
        }
    }
    public class BoolElement : BaseElement
    {
        public bool Value { get; private set; }

        public BoolElement(string name, bool value)
            : base(name)
        {
            Value = value;
        }
    }

    public class ComplexElement : BaseElement
    {
        public List<BaseElement>? Properties { get; set; }

        public ComplexElement(string name)
            : base(name)
        {

        }
    }

    public class ListElement<T> : BaseElement
    {
        public HashSet<T>? Items { get; set; }

        public ListElement(string name)
            : base(name)
        {
            Items = new();
        }
    }
    //public class StringListElement : ListElement<string>
    //{
    //    public StringListElement(string name)
    //    : base(name)
    //    {
 
    //    }
    //}
    //public class NumberListElement : ListElement<decimal>
    //{
    //    public NumberListElement(string name)
    //    : base(name)
    //    {

    //    }
    //}
    //public class BoolListElement : BaseElement
    //{
    //    public List<BoolElement>? Items { get; set; }

    //    public BoolListElement(string name)
    //        : base(name)
    //    {

    //    }
    //}
    public class ComplexListElement : BaseElement
    {
        public List<BaseElement>? Items { get; set; }

        public ComplexListElement(string name)
            : base(name)
        {
            Items = new();
        }
    }


    public class DefElement : ComplexElement
    {

        /// <summary>
        /// e.g. BodyDef
        /// </summary>
        public string DefTypeName => Name;
        /// <summary>
        /// e.g. Ersk_Duros (body)
        /// </summary>
        public string DefName { get; private set; }


        public DefElement(string defTypeName, string DefName)
            : base(defTypeName)
        {

        }
    }
}
