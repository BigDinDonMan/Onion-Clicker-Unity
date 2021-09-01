using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "New Upgrade")]
public class GameUpgrade : ScriptableObject
{
    public static readonly ulong NO_GENERATOR_ID = 9999999;
    public static readonly ulong NO_INCREASE_PER_N_OF_GENERATOR_ID = 9999998;

    public enum UpgradeType : byte {
        Click, //increase click multiplier
        Generator, //increase generator multiplier
        IncreasePerNGenerators, //increase generator mulitplier by N of other generator
        GlobalIncreasePerNGenerators, //increase global multiplier by N of one of the generators
        Global //increase global multiplier
    }
    public UpgradeType upgradeType;
    public ulong targetGeneratorID;
    public ulong increasePerNOfGeneratorID;
    public string upgradeName;
    public string description;
    public string flavorText;
    public double upgradeCost;
    public double multiplier;
    public Sprite upgradeIcon;
}
