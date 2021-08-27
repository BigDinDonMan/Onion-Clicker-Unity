using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneratorUpgrade : ScriptableObject
{
    public string upgradeName;
    public string upgradableName; //name of generator to upgrade
    public string description;
    public string flavorText;
    public double upgradeCost;
    public Sprite upgradeIcon;

    public abstract void ApplyUpgrade();
}
