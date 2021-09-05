using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

public class GeneralUtilityTool : Editor
{
    
    [MenuItem("File/Game/Data/Clear save file data", false, -1)]
    public static void ClearSaveData() {
        var saveFilePath = System.IO.Path.Combine(Application.persistentDataPath, "gamedata.dat");
        System.IO.File.Delete(saveFilePath);
    }

    [MenuItem("File/Scriptable objects/Auto-assign IDs/Achievements")]
    public static void AutoAssignAchievementIDs() {
        ulong startID = 0;
        var achievements = Resources.LoadAll<Achievement>("Achievements");
        foreach (var achievement in achievements) {
            achievement.ID = startID++;
        }
        Debug.Log($"Achievement IDs assigned successfully. {achievements.Length} achievements edited.");
    }

    [MenuItem("File/Scriptable objects/Auto-assign IDs/Upgrades")]
    public static void AutoAssignUpgradesIDs() {
        ulong startID = 0;
        var upgrades = Resources.LoadAll<GameUpgrade>("Upgrades");
        foreach (var upgrade in upgrades) {
            upgrade.ID = startID++;
        }
        Debug.Log($"Upgrade IDs assigned successfully. {upgrades.Length} upgrades edited.");
    }

    [MenuItem("File/Scriptable objects/Auto-assign IDs/Generators")]
    public static void AutoAssignGeneratorsIDs() {
        ulong startID = 0;
        var gens = Resources.LoadAll<OnionGenerator>("Generators");
        foreach (var generator in gens) {
            generator.ID = startID++;
        }
        Debug.Log($"Generator IDs assigned successfully. {gens.Length} generators edited.");
    }

    [MenuItem("File/Scriptable objects/Auto-assign IDs/All", false, -1)]
    public static void AutoAssignIDs() {
        AutoAssignAchievementIDs();
        AutoAssignGeneratorsIDs();
        AutoAssignUpgradesIDs();
    }
}
