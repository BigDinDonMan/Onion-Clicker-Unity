using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SavedStateLoader : MonoBehaviour
{
    [SerializeField]
    private SavedState savedState;

    public PlayerDetails playerDetails;
    public UpgradesManager upgradesManager;
    public AchievementManager achievementManager;

    public GameObject detailsParent;
    public List<GeneratorDetails> generatorDetails;

    private void Awake() {
        savedState = LoadSavedStateData();
    }

    private void Start() {
        generatorDetails.AddRange(detailsParent.GetComponentsInChildren<GeneratorDetails>());
        SetUpFromLoadedState();
    }

    private SavedState LoadSavedStateData() {
        var path = System.IO.Path.Combine(Application.persistentDataPath, "gamedata.dat");
        return System.IO.File.Exists(path) ? JsonUtility.FromJson<SavedState>(System.IO.File.ReadAllText(path)) : null;
    }

    private void SetUpFromLoadedState() {
        if (savedState == null) return;
        SetUpPlayerDetails();
        SetUpGeneratorDetails();
        ReapplyBoughtUpgrades();
        SetUpUnlockedAchievements();
        RespawnUnlockedUpgrades();
    }

    private void SetUpPlayerDetails() { //todo: finish this, not all data is assigned back
        playerDetails.ChangeOnions(savedState.totalOnions);
        playerDetails.TotalOnionsClicked = savedState.totalOnionsClicked;
        playerDetails.TotalOnionsEarned = savedState.totalOnionsEarned;
        playerDetails.TotalOnionsSpent = savedState.totalOnionsSpent;
        playerDetails.TotalOnionsClicked = savedState.totalOnionsClicked;
        playerDetails.TotalClicks = savedState.totalClicks;
    }

    private void SetUpGeneratorDetails() {//basing on total onions earned, unlock generators in the UI and assign values in the UI like amount, current prices, etc.
        var totalOnions = savedState.totalOnionsEarned;
        generatorDetails.ForEach(detail => {
            var shouldUnlock = totalOnions >= detail.generator.unlockedAtTotalOnions;
            if (!shouldUnlock) return;
            var genData = savedState.boughtGeneratorsData.Find(d => d.generatorID == detail.generator.ID);
            detail.IncreaseAmount(genData.amount);
            detail.UpdateGeneratorUI();
            detail.RecalculatePrices();
        });
    }

    private void ReapplyBoughtUpgrades() {//reapply all bought upgrades + set up event listeners
        savedState.boughtUpgradesIDs.ForEach(ID => {
            var upgrade = upgradesManager.allUpgrades.Find(u => u.ID == ID);
            upgradesManager.Buy(upgrade);
        });
    }

    private void SetUpUnlockedAchievements() {//that is self-explanatory, just add unlocked achievements in achievement manager
        savedState.unlockedAchievementsData.ForEach(achievementManager.unlockedAchievements.Add);
    }

    private void RespawnUnlockedUpgrades() {//spawn upgrades that are unlocked but not yet bought
        savedState.unlockedUpgradesIDs.ForEach(ID => {
            var upgrade = upgradesManager.allUpgrades.Find(u => u.ID == ID);
            upgradesManager.Unlock(upgrade);
        });
    }
}
