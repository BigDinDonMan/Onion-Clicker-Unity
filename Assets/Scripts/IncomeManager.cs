using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class IncomeManager : MonoBehaviour
{
    public RectTransform detailsParent;

    public TextMeshProUGUI currentOnionsText;
    public TextMeshProUGUI onionsPerSecondText;
    public List<GeneratorDetails> generatorDetailsList;

    public List<Achievement> globalTypeAchievements;
    public List<GameUpgrade> globalUpgrades;

    private WaitForSeconds waitTime;

    private PlayerDetails playerDetails;
    private IncomeSuffixMap suffixMap;

    private void Awake() {
        playerDetails = PlayerDetails.instance;
        suffixMap = IncomeSuffixMap.instance;
        waitTime = new WaitForSeconds(10f / 60f); //wait 10 frames
    }

    void Start()
    {
        generatorDetailsList.AddRange(detailsParent.GetComponentsInChildren<GeneratorDetails>());
        playerDetails.OnOnionsChanged += this.UpdateDisplay;
        playerDetails.OnOnionsChanged += () => generatorDetailsList.ForEach(detail => detail.UpdateButtonsEnabled());
        UpdateDisplay();
        StartCoroutine(CheckForGlobalAchievements());
        StartCoroutine(CheckForUpgradesUnlock());
    }

    void Update()
    {
        CalculateFrameIncome();
        CheckForGeneratorUnlock();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)) {
            playerDetails.ChangeOnions(100);
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            playerDetails.ChangeOnions(1_000_000);
        }

        if (Input.GetKeyDown(KeyCode.RightShift)) {
            playerDetails.ChangeOnions(100_000_000);
        }
#endif
    }

    public void UpdateDisplay() {
        var currentOnionsStr = $"{suffixMap.MapToString(playerDetails.Onions)} onions";
        var onionsPerSecondStr = $"Per second: {suffixMap.MapToString(generatorDetailsList.Sum(genDetails => genDetails.generatorAmount * genDetails.incomePerGenerator * genDetails.TotalMultiplier))}";
        onionsPerSecondText.text = onionsPerSecondStr;
        currentOnionsText.text = currentOnionsStr;
    }

    private void CalculateFrameIncome() {
        var totalFrameIncome = generatorDetailsList.Sum(detail => detail.generatorAmount * detail.incomePerGenerator * Time.deltaTime * detail.TotalMultiplier) * playerDetails.GlobalIncomeMultiplier;
        playerDetails.ChangeOnions(totalFrameIncome);
    }

    private void CheckForGeneratorUnlock() {

    }

    private IEnumerator CheckForGlobalAchievements() {
        while (true) {
            var unlockableAchievements = globalTypeAchievements.Where(a => AchievementManager.instance.IsUnlocked(a) == false);
            var triggerData = new AchievementTriggerData() {
                totalGeneratorAmount = (uint)generatorDetailsList.Sum(detail => detail.generatorAmount),
                totalClickOnions = playerDetails.TotalOnionsClicked,
                totalClicks = playerDetails.TotalClicks,
                totalOnions = playerDetails.TotalOnionsEarned
            };
            foreach (var achievement in unlockableAchievements) {
                var success = achievement.Unlock(triggerData);
                if (success) {
                    UIActions.instance.SpawnAchievementPopUp(achievement);
                }
            }
            yield return waitTime;
        }
    }

    private IEnumerator CheckForGeneratorsUnlock() {
        yield return waitTime;
    }

    private IEnumerator CheckForUpgradesUnlock() {
        while (true) {
            var lockedUpgrades = globalUpgrades.Where(u => UpgradesManager.instance.IsUnlocked(u) == false);
            var totalGeneratorAmount = generatorDetailsList.Sum(detail => detail.generatorAmount);
            var totalOnions = playerDetails.TotalOnionsEarned;
            var totalClicks = playerDetails.TotalClicks;
            var upgradeData = new GlobalUpgradeTriggerData() { 
                totalClicks = totalClicks,
                totalOnions = totalOnions,
                totalGeneratorsAmount = (ulong)totalGeneratorAmount
            };
            foreach (var upgrade in lockedUpgrades) {
                UpgradesManager.instance.Unlock(upgrade, upgradeData);
            }
            yield return waitTime;
        }
    }
}
