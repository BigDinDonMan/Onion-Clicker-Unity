using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
