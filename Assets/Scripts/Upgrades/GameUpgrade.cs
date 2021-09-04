using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "New Upgrade")]
public class GameUpgrade : ScriptableObject
{
    public static readonly ulong NO_GENERATOR_ID = 9999999;
    public static readonly ulong NO_INCREASE_PER_N_OF_GENERATOR_ID = 9999998;
    public static readonly ulong NOT_VALID = 999999;

    public enum UpgradeType : byte {
        Click, //increase click multiplier
        Generator, //increase generator multiplier
        IncreasePerNGenerators, //increase generator mulitplier by N of other generator
        GlobalIncreasePerNGenerators, //increase global multiplier by N of all generators (e.g. 1% per 100 total)
        Global //increase global multiplier
    }

    public ulong ID;
    public UpgradeType upgradeType;
    public ulong targetGeneratorID;
    public ulong increasePerNOfGeneratorID;
    public ulong perN;//number per which to increase multiplier by this object's multiplier, NOT_VALID if not used
    public string upgradeName;
    public string description;//by the way, unity supports rich text, so USE IT
    public string flavorText;
    public double upgradeCost;
    public double multiplier;
    public double unlockedAtTotalOnions; //-1 if not used
    public uint unlockedAtGenerators; //NOT_VALID if not used
    public uint unlockedAtTotalGenerators; //NOT_VALID if not used
    public ulong unlockedAtTotalClicks; //NOT_VALID if not used
    public Sprite upgradeIcon;
}
