using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SavedStatePersistor : MonoBehaviour
{
    public GameObject detailsParent;
    public List<GeneratorDetails> details;
    public UpgradesManager upgradesManager;
    public AchievementManager achievementManager;
    public PlayerDetails playerDetails;

    [SerializeField]
    private float saveInterval;

    private WaitForSeconds waitTime;

    private void Awake() {
        details.AddRange(detailsParent.GetComponentsInChildren<GeneratorDetails>());
        waitTime = new WaitForSeconds(saveInterval);
    }

    //use Application.persistentDataPath to save the game state
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SaveCurrentGameState());
    }

    private IEnumerator SaveCurrentGameState() {
        while (true) {
            yield return waitTime;
            PersistData();
            UIActions.instance.SpawnSavePopUp();
        }
    }

    private void PersistData() {
        var state = GatherSaveData();
        var path = System.IO.Path.Combine(Application.persistentDataPath, "gamedata.dat");
        System.IO.File.WriteAllText(path, JsonUtility.ToJson(state));
    }

    private SavedState GatherSaveData() {
        var state = new SavedState {
            totalClicks = playerDetails.TotalClicks,
            totalOnions = playerDetails.Onions,
            totalOnionsClicked = playerDetails.TotalOnionsClicked,
            totalOnionsSpent = playerDetails.TotalOnionsSpent,
            totalOnionsEarned = playerDetails.TotalOnionsEarned
        };
        state.unlockedAchievementsData.AddRange(achievementManager.unlockedAchievements);
        state.unlockedUpgradesIDs.AddRange(upgradesManager.unlockedUpgrades.Select(u => u.ID));
        state.boughtUpgradesIDs.AddRange(upgradesManager.boughtUpgrades.Select(u => u.ID));
        state.boughtGeneratorsData.AddRange(
            details.Select(d => new SavedState.GeneratorState() { amount = d.generatorAmount, generatorID = d.generator.ID })
        );
        return state;
    }
}
