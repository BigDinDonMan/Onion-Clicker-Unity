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

    public void Unlock() {

    }

    public void Buy() {

    }

    public bool IsUnlocked(GameUpgrade upgrade) {
        return unlockedUpgrades.Contains(upgrade);
    }

    public bool IsBought(GameUpgrade upgrade) {
        return boughtUpgrades.Contains(upgrade);
    }
}
