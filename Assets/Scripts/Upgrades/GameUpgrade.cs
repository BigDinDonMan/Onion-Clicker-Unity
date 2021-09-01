using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameUpgrade : ScriptableObject
{
    public enum UpgradeType : byte {
        Click,
        Generator,
        IncreasePerNGenerators,
        Global
    }
    public UpgradeType upgradeType;
    public string upgradeName;
    public string description;
    public string flavorText;
    public double upgradeCost;
    public double multiplier;
    public Sprite upgradeIcon;
    public OnionGenerator generator;
}
