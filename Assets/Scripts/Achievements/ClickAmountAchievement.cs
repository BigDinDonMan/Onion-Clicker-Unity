using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement Data", menuName = "Achievements/Click Amount Achievement")]
public class ClickAmountAchievement : Achievement {
    public override bool Unlock(AchievementTriggerData triggerData) {
        if (unlocked) return false;

        if (triggerData.totalClicks >= this.triggerData.totalClicks) {
            UnlockInternal();
            return true;
        }

        return false;
    }
}
