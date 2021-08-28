using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Income/Generators", fileName = "New Income Generator")]
public class OnionGenerator : ScriptableObject
{
    public string generatorName;
    public string description;
    public int basePrice;
    public double baseIncome;
    public double coefficient;
    public Sprite generatorIcon;

    public override bool Equals(object other) {
        if (ReferenceEquals(other, this)) return true;
        if (other is OnionGenerator gen)
            return gen.generatorName == this.generatorName;
        return false;
    }

    public override int GetHashCode() {
        return generatorName.GetHashCode();
    }

    public static bool operator==(OnionGenerator gen1, OnionGenerator gen2) {
        return gen1.generatorName == gen2.generatorName;
    }

    public static bool operator!=(OnionGenerator gen1, OnionGenerator gen2) {
        return gen1.generatorName != gen2.generatorName;
    }
}
