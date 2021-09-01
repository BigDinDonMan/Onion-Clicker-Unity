using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;

    public List<GameUpgrade> allUpgrades;
    public List<GameUpgrade> unlockedUpgrades;
    public List<GameUpgrade> boughtUpgrades;

    private PlayerDetails playerDetails;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        playerDetails = PlayerDetails.instance;
    }

    public void Unlock(GameUpgrade upgrade) {

    }

    public void Buy(GameUpgrade upgrade) {
        if (IsBought(upgrade)) return;
        boughtUpgrades.Add(upgrade);
        unlockedUpgrades.Remove(upgrade);

        switch (upgrade.upgradeType) {
            case GameUpgrade.UpgradeType.Click:
                break;
            case GameUpgrade.UpgradeType.Generator:
                break;
            case GameUpgrade.UpgradeType.IncreasePerNGenerators:
                break;
            case GameUpgrade.UpgradeType.GlobalIncreasePerNGenerators:
                break;
            case GameUpgrade.UpgradeType.Global:
                break;
        }
    }

    public bool IsUnlocked(GameUpgrade upgrade) {
        return unlockedUpgrades.Contains(upgrade);
    }

    public bool IsBought(GameUpgrade upgrade) {
        return boughtUpgrades.Contains(upgrade);
    }
}
