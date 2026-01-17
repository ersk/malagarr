using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse_Backstories
{
    //public enum SlotEnum
    //{
    //    Childhood,
    //    Adulthood   
    //}
    //public enum BodyType
    //{
    //    Male,
    //    Female,
    //    Thin,
    //    Fat,
    //    Hulk
    //}

    public class Back
    {
        public BackstoryDefOLD? BackstoryDef;
    }

    public class BackstoryDefOLD
    {

        public string? DefName { get; set; }
        public bool IgnoreIllegalLabelCharacterConfigError { get; set; }
        public string? Identifier { get; set; }
        public SlotEnum Slot { get; set; }
        public string? Title { get; set; }
        public string? TitleShort { get; set; }
        public string? BaseDesc { get; set; }
        public List<Li>? SkillGains { get; set; }
        public string? WorkDisables { get; set; }
        public string? RequiredWorkTags { get; set; }
        public List<SpawnCategory>? SpawnCategories { get; set; }
        public BodyType BodyTypeGlobal { get; set; }
        public BodyType BodyTypeFemale { get; set; }
        public BodyType BodyTypeMale { get; set; }
        public bool? Shuffleable { get; set; }

        public class SpawnCategory {
            public string? Li { get; set; }
        }

        public class Li
        {
            public string? Key { get; set; }
            public int Value { get; set; }
        }
    }


}
