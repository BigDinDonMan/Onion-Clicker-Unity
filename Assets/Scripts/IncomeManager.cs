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

    private PlayerDetails playerDetails;

    private void Awake() {
        playerDetails = PlayerDetails.instance;
        achievementWaitTime = new WaitForSeconds(10f / 60f); //wait 10 frames
    }

    void Start()
    {
        generatorDetailsList.AddRange(detailsParent.GetComponentsInChildren<GeneratorDetails>());
        playerDetails.OnOnionsChanged += this.UpdateDisplay;
        UpdateDisplay();
        StartCoroutine(CheckForGlobalAchievements());
    }

    void Update()
    {
        CalculateFrameIncome();
        CheckForGeneratorUnlock();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space)) {
            playerDetails.ChangeOnions(100);
            generatorDetailsList.ForEach(detail => detail.UpdateButtonsEnabled());
        }
#endif
    }

    public void UpdateDisplay() {
        var currentOnionsStr = $"{playerDetails.Onions:0} onions";
        var onionsPerSecondStr = $"{generatorDetailsList.Sum(genDetails => genDetails.generatorAmount * genDetails.incomePerGenerator):0.0} onions per second";
        onionsPerSecondText.text = onionsPerSecondStr;
        currentOnionsText.text = currentOnionsStr;
    }

    private void CalculateFrameIncome() {
        var totalFrameIncome = generatorDetailsList.Sum(detail => detail.generatorAmount * detail.incomePerGenerator * Time.deltaTime) * playerDetails.GlobalIncomeMultiplier;
        playerDetails.ChangeOnions(totalFrameIncome);
        generatorDetailsList.ForEach(detail => detail.UpdateButtonsEnabled());
    }

    private void CheckForGeneratorUnlock() {

    }

    private IEnumerator CheckForGlobalAchievements() {
        while (true) {
            Debug.Log("checking for achievements...");
            var unlockableAchievements = globalTypeAchievements.Where(a => a.unlocked == false);
            var triggerData = new AchievementTriggerData() {
                totalGeneratorAmount = (uint)generatorDetailsList.Sum(detail => detail.generatorAmount),
                totalClickOnions = playerDetails.TotalOnionsClicked,
                totalClicks = playerDetails.TotalClicks,
                totalOnions = playerDetails.TotalOnionsEarned
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
