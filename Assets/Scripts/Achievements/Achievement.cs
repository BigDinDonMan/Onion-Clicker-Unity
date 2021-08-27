using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Achievement : ScriptableObject
{
    public string achievementName;
    public string achievementDescription;
    public string flavorText;
    public AchievementTriggerData triggerData;
    public bool unlocked;
    public Sprite achievementIcon;

    public abstract void Unlock();
}
