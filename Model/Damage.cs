public class Def
{
    public string Name;
    public string DefName;
    public string Label;
    public string? ParentName;
}
public class Damage
{
    public string WorkerClass;
    public bool ExternalViolence;
    public string DeathMessage;
    public string Hediff;
    public string HediffSkin;
    public string HediffSolid;
    public bool HarmAllLayersUntilOutside;
    public string ImpactSoundType;
    public string ArmorCategory;
    //range convert to string "0~0.1"
    public RangeString OverkillPctToDestroyPart;
    public Curve CutExtraTargetsCurve;
    public decimal CutCleaveBonus;

    public decimal BluntStunDuration;
    public decimal BluntInnerHitChance;
    public RangeString BluntInnerHitDamageFractionToConvert;
    public RangeString BluntInnerHitDamageFractionToAdd;
    public Curve BluntStunChancePerDamagePctOfCorePartToHeadCurve;
    public Curve BluntStunChancePerDamagePctOfCorePartToBodyCurve;
}
public class RangeString
{
    public decimal From;
    public decimal To;
}
public class Range
{
    public decimal Min;
    public decimal Max;
}
public class Curve
{
    public List<Point> Points;
}