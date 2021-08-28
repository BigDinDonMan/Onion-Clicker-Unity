using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Global Generator Amount Achievement", fileName = "New Achievement Data")]
public class GlobalGeneratorAmountAchievement : Achievement {
    public override bool Unlock(AchievementTriggerData triggerData) {
        if (unlocked) return false;

        if (triggerData.totalGeneratorAmount >= this.triggerData.totalGeneratorAmount) {
            UnlockInternal();
            return true;
        }

        return false;
    }
}
