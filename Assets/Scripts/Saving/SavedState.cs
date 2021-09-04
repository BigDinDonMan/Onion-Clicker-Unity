using System.Collections.Generic;

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
    public double totalOnionsEarned;
    public double totalOnionsSpent;
    public double totalOnionsClicked;
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
