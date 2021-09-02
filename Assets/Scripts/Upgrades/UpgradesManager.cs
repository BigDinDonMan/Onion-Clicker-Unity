using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;

    public List<GameUpgrade> unlockedUpgrades;
    public List<GameUpgrade> boughtUpgrades;

    private PlayerDetails playerDetails;

    public List<GeneratorDetails> generatorDetails;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        playerDetails = PlayerDetails.instance;
    }

    public bool Unlock(GameUpgrade upgrade, GlobalUpgradeTriggerData? data = null) {
        if (IsUnlocked(upgrade)) return false;
        if (data != null) { //global upgrade
            var actualData = data.Value;
            switch (upgrade.upgradeType) {//types not included in here are generator specific and will be handled in gen details
                case GameUpgrade.UpgradeType.Global:
                    if (!(actualData.totalOnions >= upgrade.unlockedAtTotalOnions && upgrade.unlockedAtTotalOnions >= 0)) {
                        return false;
                    }
                    break;
                case GameUpgrade.UpgradeType.Click:
                    if (!(actualData.totalClicks >= upgrade.unlockedAtTotalClicks && upgrade.unlockedAtTotalClicks != GameUpgrade.NOT_VALID)) {
                        return false;
                    }
                    break;
                case GameUpgrade.UpgradeType.GlobalIncreasePerNGenerators:
                    if (!(actualData.totalGeneratorsAmount >= upgrade.unlockedAtTotalGenerators && upgrade.unlockedAtTotalGenerators != GameUpgrade.NOT_VALID)) {
                        return false;
                    }
                    break;
            }
        }//else its a generator upgrade (its checked for unlocks from outside this function)
        unlockedUpgrades.Add(upgrade);
        UIActions.instance.SpawnGeneratorUpgradeDetailsWindow(upgrade);
        return true;
    }

    public void Buy(GameUpgrade upgrade) {
        if (IsBought(upgrade)) return;
        boughtUpgrades.Add(upgrade);

        switch (upgrade.upgradeType) {
            case GameUpgrade.UpgradeType.Click:
                playerDetails.IncreaseClickIncome(upgrade.multiplier);
                break;
            case GameUpgrade.UpgradeType.Generator:
                var details = generatorDetails.Find(d => d.generator.ID == upgrade.targetGeneratorID);
                details.incomeGeneratorMultiplier *= upgrade.multiplier;
                break;
            case GameUpgrade.UpgradeType.IncreasePerNGenerators:
                GeneratorDetails genToIncreaseDetails = generatorDetails.Find(d => d.generator.ID == upgrade.targetGeneratorID);
                GeneratorDetails genToIncreaseByNOfDetails = generatorDetails.Find(d => d.generator.ID == upgrade.increasePerNOfGeneratorID);
                break;
            case GameUpgrade.UpgradeType.GlobalIncreasePerNGenerators:
                break;
            case GameUpgrade.UpgradeType.Global:
                break;
        }
    }

    public bool IsUnlocked(GameUpgrade upgrade) => unlockedUpgrades.Contains(upgrade);

    public bool IsBought(GameUpgrade upgrade) => boughtUpgrades.Contains(upgrade);
}
