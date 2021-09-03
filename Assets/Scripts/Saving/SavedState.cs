using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//save: generator data (list of structs)
//unlocked achievements (list of int IDs/names)
//unlocked and bought upgrades (?)
[System.Serializable]
public class SavedState
{
    [System.Serializable]
    public class GeneratorState {
        public ulong generatorID;
        public uint amount;
    }

    public List<GeneratorState> boughtGeneratorsData;
    public List<AchievementUnlockData> unlockedAchievementsData;
    public List<ulong> unlockedUpgradesIDs;
    public List<ulong> boughtUpgradesIDs;
    public double totalOnions;
    public double totalClickOnions;
    public double totalOnionsEarned;
    public double totalOnionsSpent;
    public ulong totalClicks;
    public SerializableTimeStamp savedAt;

    public SavedState() {
        boughtGeneratorsData = new List<GeneratorState>();
        unlockedAchievementsData = new List<AchievementUnlockData>();
        unlockedUpgradesIDs = new List<ulong>();
        boughtUpgradesIDs = new List<ulong>();
        savedAt = System.DateTime.Now;
    }
}
