using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;

    public List<GeneratorUpgrade> allUpgrades;
    public List<GeneratorUpgrade> unlockedUpgrades;
    public List<GeneratorUpgrade> boughtUpgrades;

    private void Awake() {
        instance = this;
    }
}
