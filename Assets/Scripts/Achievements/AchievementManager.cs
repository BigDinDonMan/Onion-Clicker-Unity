using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    public List<Achievement> allAchievements;
    public List<AchievementUnlockData> unlockedAchievements;

    private void Awake() {
        instance = this;
        allAchievements = Resources.LoadAll<Achievement>("Achievements").ToList();
    }

    public void Unlock(Achievement a) {
        if (IsUnlocked(a)) return;
        unlockedAchievements.Add(new AchievementUnlockData() { achievementID = a.ID, unlockedAt = System.DateTime.Now });
    }

    public bool IsUnlocked(Achievement a) => unlockedAchievements.Any(_a => a.ID == _a.achievementID);
}
