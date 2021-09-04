using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStateLoader : MonoBehaviour
{
    [SerializeField]
    private SavedState savedState;

    public PlayerDetails playerDetails;
    public UpgradesManager upgradesManager;
    public AchievementManager achievementManager;

    public GameObject upgradesParent;
    public GameObject detailsParent;
    public List<GeneratorDetails> generatorDetails;

    private void Awake() {
        savedState = LoadSavedStateData();
        SetUpFromLoadedState();
    }

    private SavedState LoadSavedStateData() {
        var path = System.IO.Path.Combine(Application.persistentDataPath, "gamedata.dat");
        return JsonUtility.FromJson<SavedState>(System.IO.File.ReadAllText(path));
    }

    private void SetUpFromLoadedState() {
        playerDetails.ChangeOnions(savedState.totalOnions);
        playerDetails.TotalOnionsClicked = savedState.totalClickOnions;
        playerDetails.TotalOnionsEarned = savedState.totalOnionsEarned;
        playerDetails.TotalOnionsSpent = savedState.totalOnionsSpent;
    }
}
