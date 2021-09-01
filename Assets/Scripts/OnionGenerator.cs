using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Income/Generators", fileName = "New Income Generator")]
public class OnionGenerator : ScriptableObject
{
    public ulong ID;
    public string generatorName;
    public string description;
    public int basePrice;
    public double baseIncome;
    public double coefficient;
    public double unlockedAtTotalOnions;
    public Sprite generatorIcon;

    public override bool Equals(object other) {
        if (ReferenceEquals(other, this)) return true;
        if (other is OnionGenerator gen)
            return gen.ID == this.ID;
        return false;
    }

    public override int GetHashCode() {
        return ID.GetHashCode();
    }

    public static bool operator==(OnionGenerator gen1, OnionGenerator gen2) {
        return gen1?.ID == gen2?.ID;
    }

    public static bool operator!=(OnionGenerator gen1, OnionGenerator gen2) {
        return !(gen1?.ID == gen2?.ID);
    }
}
