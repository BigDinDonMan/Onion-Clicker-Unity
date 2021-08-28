using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Generator Amount Achievement", fileName = "New Achievement Data")]
public class GeneratorAmountAchievement : Achievement {
    public override bool Unlock(AchievementTriggerData triggerData) {
        if (this.unlocked || triggerData.generator == null) return false;
        if (this.triggerData.generator != triggerData.generator) return false;

        if (triggerData.generatorAmount >= this.triggerData.generatorAmount) {
            UnlockInternal();
            return true;
        }

        return false;
    }
}
