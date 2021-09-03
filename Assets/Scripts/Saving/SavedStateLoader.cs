using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStateLoader : MonoBehaviour
{
    [SerializeField]
    private SavedState savedState;

    public PlayerDetails playerDetails;
    public UpgradesManager upgradesManager;

    private void Awake() {
        savedState = LoadSavedStateData();
        SetUpFromLoadedState();
    }

    private SavedState LoadSavedStateData() {
        var path = System.IO.Path.Combine(Application.persistentDataPath, "gamedata.dat");
        return JsonUtility.FromJson<SavedState>(System.IO.File.ReadAllText(path));
    }

    private void SetUpFromLoadedState() {
        Debug.Log($"{savedState.boughtGeneratorsData}  {savedState.savedAt}  {savedState.totalClicks}  {savedState.totalOnions}");
    }
}
