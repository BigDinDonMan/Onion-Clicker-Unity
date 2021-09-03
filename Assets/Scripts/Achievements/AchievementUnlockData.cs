using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AchievementUnlockData
{
    public ulong achievementID;
    public SerializableTimeStamp unlockedAt;

    public AchievementUnlockData(ulong id, System.DateTime unlockedAt) {
        achievementID = id;
        this.unlockedAt = new SerializableTimeStamp(unlockedAt);
    }
}
