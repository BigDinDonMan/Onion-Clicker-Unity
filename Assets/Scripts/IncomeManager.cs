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

    private WaitForSeconds achievementWaitTime;

    private void Awake() {
        achievementWaitTime = new WaitForSeconds(10f / 60f); //wait 10 frames
    }

    void Start()
    {
        generatorDetailsList.AddRange(detailsParent.GetComponentsInChildren<GeneratorDetails>());
        PlayerDetails.instance.OnOnionsChanged += this.UpdateDisplay;
        UpdateDisplay();
        StartCoroutine(CheckForGlobalAchievements());
    }

    void Update()
    {
        var totalFrameIncome = generatorDetailsList.Sum(detail => detail.generatorAmount * detail.incomePerGenerator * Time.deltaTime) * PlayerDetails.instance.GlobalIncomeMultiplier;
        PlayerDetails.instance.ChangeOnions(totalFrameIncome);
        generatorDetailsList.ForEach(detail => detail.UpdateButtonsEnabled());
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)) {
            PlayerDetails.instance.ChangeOnions(100);
            generatorDetailsList.ForEach(detail => detail.UpdateButtonsEnabled());
        }
#endif
    }

    public void UpdateDisplay() {
        var currentOnionsStr = $"{PlayerDetails.instance.Onions:0} onions";
        var onionsPerSecondStr = $"{generatorDetailsList.Sum(genDetails => genDetails.generatorAmount * genDetails.incomePerGenerator):0.0} onions per second";
        onionsPerSecondText.text = onionsPerSecondStr;
        currentOnionsText.text = currentOnionsStr;
    }

    private IEnumerator CheckForGlobalAchievements() {
        while (true) {
            Debug.Log("checking for achievements...");
            var unlockableAchievements = globalTypeAchievements.Where(a => a.unlocked == false);
            var triggerData = new AchievementTriggerData() {
                totalGeneratorAmount = (uint)generatorDetailsList.Sum(detail => detail.generatorAmount),
                totalClickOnions = PlayerDetails.instance.TotalOnionsClicked,
                totalClicks = PlayerDetails.instance.TotalClicks,
                totalOnions = PlayerDetails.instance.TotalOnionsEarned
            };
            foreach (var achievement in unlockableAchievements) {
                var success = achievement.Unlock(triggerData);
                if (success) {
                    //todo: show achievement popup here
                    //todo: also create a sliding animation for these bad boys
                    Debug.Log($"Unlocked achievement: {achievement.achievementName}, at: {achievement.unlockedAt}");
                    UIActions.instance.SpawnAchievementPopUp(achievement);
                }
            }
            yield return achievementWaitTime;
        }
    }
}
