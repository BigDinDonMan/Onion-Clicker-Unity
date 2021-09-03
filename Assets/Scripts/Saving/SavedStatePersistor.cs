using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedStatePersistor : MonoBehaviour
{
    public GameObject detailsParent;
    public List<GeneratorDetails> details;
    public List<Achievement> unlockedAchievements;
    public List<GameUpgrade> unlockedUpgrades;
    public List<GameUpgrade> boughtUpgrades;

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
            //save game here
            PersistData();
            //todo: show popup with "Game saved"
            UIActions.instance.SpawnSavePopUp();
        }
    }

    private void PersistData() {
        var state = new SavedState();

        using var fs = new FileStream(Path.Combine(Application.persistentDataPath, "gamedata.dat"), FileMode.OpenOrCreate); 
        using var writer = new StreamWriter(fs); 
        writer.Write(JsonUtility.ToJson(state));
    }
}
