using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Achievement : ScriptableObject
{
    public ulong ID;
    public string achievementName;
    public string achievementDescription;
    public string flavorText;
    public AchievementTriggerData triggerData;
    public Sprite achievementIcon;

    public abstract bool Unlock(AchievementTriggerData triggerData);

    protected void UnlockInternal() {
        AchievementManager.instance.Unlock(this);
    }
}
