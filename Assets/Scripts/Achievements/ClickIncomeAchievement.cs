using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement Data", menuName = "Achievements/Click Income Achievement")]

public class ClickIncomeAchievement : Achievement {
    public override bool Unlock(AchievementTriggerData triggerData) {
        if (unlocked) return false;

        if (triggerData.totalClickOnions >= this.triggerData.totalClickOnions) {
            UnlockInternal();
            return true;
        }

        return false;
    }
}
