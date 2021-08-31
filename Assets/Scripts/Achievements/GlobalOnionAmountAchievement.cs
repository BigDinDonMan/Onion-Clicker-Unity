using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement Data", menuName = "Achievements/Total Onion Amount Achievement")]
public class GlobalOnionAmountAchievement : Achievement {
    public override bool Unlock(AchievementTriggerData triggerData) {
        if (AchievementManager.instance.IsUnlocked(this)) return false;

        if (triggerData.totalOnions >= this.triggerData.totalOnions) {
            UnlockInternal();
            return true;
        }
        return false;
    }
}
