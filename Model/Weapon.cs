
public class ThingDefModel
{
    public string ParentName { get; set; }
}
public class GraphicData
{
    public string TexPath;
    public string GraphicClass;
}
public class StatBases
{
    public int WorkToMake;
    public int Mass;
    public int RangedWeapon_Cooldown;
}
public class WeaponStatBases : StatBases
{
public decimal AccuracyTouch;
public decimal AccuracyShort;
public decimal AccuracyMedium;
public decimal AcvuracyLong;
}
publi class RecipeMaker
{
}
public class WeaponTool
{
    public string Label;
    public List<string> Capacities;
    public int Power;
    public int CooldownTime;
}
public class WeaponModel : ThingDefModel
{
public string DefName;
public string Label;
public string Description;
public string TechLevel;
public GraphicData GraphicData;
public string SoundInteract;
public int CostStuffCount;
public int EquippedAngleOffset;
public List<string> StuffCategories;
public List<string> WeaponTags;
public List<string> WeaponClasses;
public List<WeaponTool> Tools;
}
public abstract class Verb
{
    public abstract string VerbClass { get; }
}
public class ShootVerb : Verb
{
    public string VerbClass => "Verb_Shoot";
    public bool HasStandardCommand;
    public string DefaultProjectile;
    public decimal WarmupTime;
    public decimal Range;
    public string SoundCast;
}