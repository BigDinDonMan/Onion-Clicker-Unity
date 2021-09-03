using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//save: generator data (list of structs)
//unlocked achievements (list of int IDs/names)
//unlocked and bought upgrades (?)
public class SavedState
{
    public class GeneratorState {
        public string generatorName;
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
}
