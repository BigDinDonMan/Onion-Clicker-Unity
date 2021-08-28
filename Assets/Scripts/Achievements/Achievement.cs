using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Achievement : ScriptableObject
{
    public string achievementName;
    public string achievementDescription;
    public string flavorText;
    public bool unlocked;
    public System.DateTime unlockedAt;
    public AchievementTriggerData triggerData;
    public Sprite achievementIcon;

    public abstract bool Unlock(AchievementTriggerData triggerData);

    protected void UnlockInternal() {
        if (unlocked) return;
        unlocked = true;
        unlockedAt = System.DateTime.Now;
    }
}
