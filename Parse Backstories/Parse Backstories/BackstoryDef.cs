using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static Parse_Backstories.BackstoryDefOLD;

namespace Parse_Backstories
{
    public enum SlotEnum
    {
        Childhood,
        Adulthood
    }
    public enum BodyType
    {
        Male,
        Female,
        Thin,
        Fat,
        Hulk
    }

    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(Defs));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (Defs)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "li")]
    public class Li
    {

        [XmlElement(ElementName = "key")]
        public string Key { get; set; }

        [XmlElement(ElementName = "value")]
        public int Value { get; set; }

        [XmlAttribute(AttributeName = "MayRequire")]
        public string MayRequire { get; set; }

        [XmlText]
        public string Text { get; set; }

        [XmlAttribute(AttributeName = "MayRequireAnyOf")]
        public string MayRequireAnyOf { get; set; }
    }

    [XmlRoot(ElementName = "skillGains")]
    public class SkillGains
    {
        public int Shooting
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Shooting");
                return li == null ? 0 : li.Value;
            }
        }
        public int Melee
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Melee");
                return li == null ? 0 : li.Value;
            }
        }
        public int Construction
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Construction");
                return li == null ? 0 : li.Value;
            }
        }
        public int Mining
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Mining");
                return li == null ? 0 : li.Value;
            }
        }
        public int Cooking
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Cooking");
                return li == null ? 0 : li.Value;
            }
        }
        public int Plants
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Plants");
                return li == null ? 0 : li.Value;
            }
        }
        public int Animals
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Animals");
                return li == null ? 0 : li.Value;
            }
        }
        public int Crafting
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Crafting");
                return li == null ? 0 : li.Value;
            }
        }
        public int Artistic
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Artistic");
                return li == null ? 0 : li.Value;
            }
        }
        public int Medical
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Medical");
                return li == null ? 0 : li.Value;
            }
        }
        public int Social
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Social");
                return li == null ? 0 : li.Value;
            }
        }
        public int Intellectual
        {
            get
            {
                Li? li = Li.FirstOrDefault(l => l.Key == "Intellectual");
                return li == null ? 0 : li.Value;
            }
        }


        [XmlElement(ElementName = "li")]
        public List<Li> Li { get; set; }
    }

    [XmlRoot(ElementName = "spawnCategories")]
    public class SpawnCategories
    {
        public override string ToString()
        {
            if (Li == null) return "";

            return String.Join(", ", Li);
        }

        [XmlElement(ElementName = "li")]
        public List<string> Li { get; set; }
    }

    [XmlRoot(ElementName = "BackstoryDef")]
    public class BackstoryDef
    {

        [XmlElement(ElementName = "defName")]
        public string DefName { get; set; }

        [XmlElement(ElementName = "ignoreIllegalLabelCharacterConfigError")]
        public SafeBool IgnoreIllegalLabelCharacterConfigError { get; set; }

        [XmlElement(ElementName = "identifier")]
        public string Identifier { get; set; }

        [XmlElement(ElementName = "slot")]
        public string Slot { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "titleShort")]
        public string TitleShort { get; set; }

        [XmlElement(ElementName = "baseDesc")]
        public string BaseDesc { get; set; }

        [XmlElement(ElementName = "skillGains")]
        public SkillGains SkillGains { get; set; }

        [XmlElement(ElementName = "workDisables")]
        public string WorkDisables { get; set; }

        [XmlElement(ElementName = "requiredWorkTags")]
        public string RequiredWorkTags { get; set; }

        [XmlElement(ElementName = "spawnCategories")]
        public SpawnCategories SpawnCategories { get; set; }

        [XmlElement(ElementName = "bodyTypeGlobal")]
        public string BodyTypeGlobal { get; set; }

        [XmlElement(ElementName = "bodyTypeFemale")]
        public string BodyTypeFemale { get; set; }

        [XmlElement(ElementName = "bodyTypeMale")]
        public string BodyTypeMale { get; set; }

        [XmlElement(ElementName = "shuffleable")]
        public SafeBool Shuffleable { get; set; }

        [XmlElement(ElementName = "forcedTraits")]
        public ForcedTraits ForcedTraits { get; set; }

        [XmlElement(ElementName = "disallowedTraits")]
        public DisallowedTraits DisallowedTraits { get; set; }

        [XmlElement(ElementName = "possessions")]
        public Possessions Possessions { get; set; }

        public static string GetCsvHeader()
        {
            return "Def Name, Slot, Title, Male Count, Female Count, Thin Count, Hulk Count, Fat Count, Base Desc, " +
                "Firefighting Disabled, Violent Disabled, Shooting, Melee";
        }
        public string ToCsvString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(DefName); sb.Append(',');
            sb.Append(Slot.ToCsvValue()); sb.Append(',');
            sb.Append(Title.ToCsvValue()); sb.Append(',');
            sb.Append(MaleCount); sb.Append(',');
            sb.Append(FemaleCount); sb.Append(',');
            sb.Append(ThinCount); sb.Append(',');
            sb.Append(HulkCount); sb.Append(',');
            sb.Append(FatCount); sb.Append(',');
            sb.Append(BaseDesc.ToCsvValue()); sb.Append(',');
            sb.Append(FirefightingDisabled ? "-" : "yes"); sb.Append(',');
            sb.Append(ViolentDisabled ? "-" : SkillGains.Shooting); sb.Append(',');
            sb.Append(ViolentDisabled ? "-" : SkillGains.Melee); sb.Append(',');
            sb.Append(ConstructionDisabled || ManualSkilledDisabled ? "-" : SkillGains.Construction); sb.Append(',');
            sb.Append(MiningDisabled || ManualSkilledDisabled ? "-" : SkillGains.Mining); sb.Append(',');
            sb.Append(CookingDisabled || ManualSkilledDisabled ? "-" : SkillGains.Cooking); sb.Append(',');
            sb.Append(PlantWorkDisabled || ManualSkilledDisabled ? "-" : SkillGains.Plants); sb.Append(',');
            sb.Append(AnimalsDisabled ? "-" : SkillGains.Animals); sb.Append(',');
            sb.Append(CraftingDisabled || ManualSkilledDisabled ? "-" : SkillGains.Crafting); sb.Append(',');
            sb.Append(ArtisticDisabled || ManualSkilledDisabled ? "-" : SkillGains.Artistic); sb.Append(',');
            sb.Append(MedicalDisabled || CaringDisabled ? "-" : SkillGains.Medical); sb.Append(',');
            sb.Append(SocialDisabled ? "-" : SkillGains.Social); sb.Append(',');
            sb.Append(IntellectualDisabled ? "-" : SkillGains.Intellectual); sb.Append(',');
            sb.Append(HaulingDisabled || ManualDumbDisabled ? "-" : "yes"); sb.Append(',');
            sb.Append(CleaningDisabled || ManualDumbDisabled ? "-" : "yes"); //sb.Append(',');

            // Cleaning
            // Firefighting
            // Hauling
            // Caring
            // ManualSkilled
            // ManualDumb

            // Violent

            // Construction
            // Mining
            // Cooking
            // PlantWork
            // Animals
            // Crafting
            // Artistic
            // Medical
            // Social
            // Intellectual

            return sb.ToString();
        }

        public string BodyType
        {
            get
            {
                if (IsSame(BodyTypeGlobal, BodyTypeMale) && IsSame(BodyTypeMale, BodyTypeFemale))
                {
                    return BodyTypeGlobal;
                }
                return "MIXED";
            }
        }
        public int MaleCount
        {
            get
            {
                int count = 0;
                if (BodyTypeGlobal != null && BodyTypeGlobal.ToLower() == "male") count++;
                if (BodyTypeMale != null && BodyTypeMale.ToLower() == "male") count++;
                if (BodyTypeFemale != null && BodyTypeFemale.ToLower() == "male") count++;
                return count;
            }
        }
        public int FemaleCount
        {
            get
            {
                int count = 0;
                if (BodyTypeGlobal != null && BodyTypeGlobal.ToLower() == "female") count++;
                if (BodyTypeMale != null && BodyTypeMale.ToLower() == "female") count++;
                if (BodyTypeFemale != null && BodyTypeFemale.ToLower() == "female") count++;
                return count;
            }
        }
        public int ThinCount
        {
            get
            {
                int count = 0;
                if (BodyTypeGlobal != null && BodyTypeGlobal.ToLower() == "thin") count++;
                if (BodyTypeMale != null && BodyTypeMale.ToLower() == "thin") count++;
                if (BodyTypeFemale != null && BodyTypeFemale.ToLower() == "thin") count++;
                return count;
            }
        }
        public int HulkCount
        {
            get
            {
                int count = 0;
                if (BodyTypeGlobal != null && BodyTypeGlobal.ToLower() == "hulk") count++;
                if (BodyTypeMale != null && BodyTypeMale.ToLower() == "hulk") count++;
                if (BodyTypeFemale != null && BodyTypeFemale.ToLower() == "hulk") count++;
                return count;
            }
        }
        public int FatCount
        {
            get
            {
                int count = 0;
                if (BodyTypeGlobal != null && BodyTypeGlobal.ToLower() == "fat") count++;
                if (BodyTypeMale != null && BodyTypeMale.ToLower() == "fat") count++;
                if (BodyTypeFemale != null && BodyTypeFemale.ToLower() == "fat") count++;
                return count;
            }
        }



        public bool IsSame(string? value1, string? value2)
        {
            if (value1 == null && value2 == null) return true;

            if (value1 == null)
            {
                if (value2 == null) return true;
                return false;
            }
            else if (value2 == null)
            {
                return false;
            }

            return value1.ToLower() == value2.ToLower();
        }

        public List<string> workDisablesList
        {
            get
            {
                List<string> list = new();
                if (string.IsNullOrWhiteSpace(WorkDisables)) return list;
                list = WorkDisables.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
                list.ForEach(wd => wd = wd.ToLower());
                List<string> newList = new();
                foreach (string wd in list)
                {
                    newList.Add(wd.ToLower());
                }
                return newList;
            }
        }
        public bool ViolentDisabled
        {
            get
            {
                return workDisablesList.Contains("violent");
            }
        }
        public bool ConstructionDisabled
        {
            get
            {
                return workDisablesList.Contains("construction");
            }
        }
        public bool MiningDisabled
        {
            get
            {
                return workDisablesList.Contains("mining");
            }
        }
        public bool CookingDisabled
        {
            get
            {
                return workDisablesList.Contains("cooking");
            }
        }
        public bool PlantWorkDisabled
        {
            get
            {
                return workDisablesList.Contains("plantwork");
            }
        }
        public bool AnimalsDisabled
        {
            get
            {
                return workDisablesList.Contains("animals");
            }
        }
        public bool CraftingDisabled
        {
            get
            {
                return workDisablesList.Contains("crafting");
            }
        }
        public bool ArtisticDisabled
        {
            get
            {
                return workDisablesList.Contains("artistic");
            }
        }
        public bool MedicalDisabled
        {
            get
            {
                return workDisablesList.Contains("medical");
            }
        }
        public bool SocialDisabled
        {
            get
            {
                return workDisablesList.Contains("social");
            }
        }
        public bool IntellectualDisabled
        {
            get
            {
                return workDisablesList.Contains("intellectual");
            }
        }


        // Cleaning
        // Firefighting
        // Hauling
        // Caring
        // ManualSkilled
        // ManualDumb
        public bool CleaningDisabled
        {
            get
            {
                return workDisablesList.Contains("cleaning");
            }
        }
        public bool FirefightingDisabled
        {
            get
            {
                return workDisablesList.Contains("firefighting");
            }
        }
        public bool HaulingDisabled
        {
            get
            {
                return workDisablesList.Contains("hauling");
            }
        }
        public bool CaringDisabled
        {
            get
            {
                return workDisablesList.Contains("caring");
            }
        }
        public bool ManualDumbDisabled
        {
            get
            {
                return workDisablesList.Contains("manualskilled");
            }
        }
        public bool ManualSkilledDisabled
        {
            get
            {
                return workDisablesList.Contains("manualskilled");
            }
        }
    }



    [XmlRoot(ElementName = "forcedTraits")]
    public class ForcedTraits
    {

        [XmlElement(ElementName = "Greedy")]
        public int Greedy { get; set; }

        [XmlElement(ElementName = "Brawler")]
        public int Brawler { get; set; }

        [XmlElement(ElementName = "Transhumanist")]
        public int Transhumanist { get; set; }

        [XmlElement(ElementName = "Kind")]
        public string? z_kind { get; set; }

        [XmlIgnore]
        public int Kind
        {
            get
            {
                if (string.IsNullOrWhiteSpace(z_kind))
                {
                    return 0;
                }
                return int.Parse(z_kind);
            }
        }

        [XmlElement(ElementName = "Abrasive")]
        public int Abrasive { get; set; }

        [XmlElement(ElementName = "Beauty")]
        public int Beauty { get; set; }

        [XmlElement(ElementName = "Bloodlust")]
        public int Bloodlust { get; set; }

        [XmlElement(ElementName = "ShootingAccuracy")]
        public int ShootingAccuracy { get; set; }

        [XmlElement(ElementName = "Psychopath")]
        public int Psychopath { get; set; }

        [XmlElement(ElementName = "FastLearner")]
        public int FastLearner { get; set; }

        [XmlElement(ElementName = "BodyPurist")]
        public int BodyPurist { get; set; }

        [XmlElement(ElementName = "Gourmand")]
        public int Gourmand { get; set; }

        [XmlElement(ElementName = "Industriousness")]
        public int Industriousness { get; set; }

        [XmlElement(ElementName = "Nimble")]
        public int Nimble { get; set; }

        [XmlElement(ElementName = "Tough")]
        public int Tough { get; set; }

        [XmlElement(ElementName = "DrugDesire")]
        public int DrugDesire { get; set; }

        [XmlElement(ElementName = "TooSmart")]
        public int TooSmart { get; set; }

        [XmlElement(ElementName = "NaturalMood")]
        public int NaturalMood { get; set; }

        [XmlElement(ElementName = "SpeedOffset")]
        public int SpeedOffset { get; set; }
    }

    [XmlRoot(ElementName = "disallowedTraits")]
    public class DisallowedTraits
    {

        [XmlElement(ElementName = "ShootingAccuracy")]
        public int ShootingAccuracy { get; set; }
    }

    [XmlRoot(ElementName = "possessions")]
    public class Possessions
    {

        [XmlElement(ElementName = "li")]
        public Li Li { get; set; }
    }

    [XmlRoot(ElementName = "Defs")]
    public class Defs
    {

        [XmlElement(ElementName = "BackstoryDef")]
        public List<BackstoryDef> BackstoryDef { get; set; }
    }


    public static class StringExtensions
    {
        public static string ToCsvValue(this string s)
        {
            if(string.IsNullOrWhiteSpace(s)) return "";
            if (s.Contains("\"") || s.Contains("\n"))
            {
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            }
            else if (s.Contains(","))
            {
                return "\"" + s + "\"";
            }

            return s;
        }
    }
}
