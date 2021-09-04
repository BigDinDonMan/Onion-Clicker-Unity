using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;

public class GeneralUtilityTool : Editor
{
    
    [MenuItem("File/Game data/Clear save file data", false, -1)]
    public static void ClearSaveData() {
        var saveFilePath = System.IO.Path.Combine(Application.persistentDataPath, "gamedata.dat");
        System.IO.File.Delete(saveFilePath);
    }
}
