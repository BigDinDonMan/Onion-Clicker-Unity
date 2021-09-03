using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStateLoader : MonoBehaviour
{
    [SerializeField]
    private SavedState savedState;

    private void Awake() {
        savedState = LoadSavedStateData();
        SetUpFromLoadedState();
    }

    private SavedState LoadSavedStateData() {
        return JsonUtility.FromJson<SavedState>(System.IO.File.ReadAllText(System.IO.Path.Combine(Application.persistentDataPath, "gamedata.dat")));
    }

    private void SetUpFromLoadedState() {
        Debug.Log($"{savedState.boughtGeneratorsData}  {savedState.savedAt}  {savedState.totalClicks}  {savedState.totalOnions}");
    }
}
