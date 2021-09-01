using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedStatePersistor : MonoBehaviour
{

    public List<GeneratorDetails> details;
    public List<Achievement> unlockedAchievements;
    public List<GeneratorUpgrade> unlockedUpgrades;
    public List<GeneratorUpgrade> boughtUpgrades;

    [SerializeField]
    private float saveInterval;

    private WaitForSeconds waitTime;

    private void Awake() {
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
        }
    }

    private void PersistData() {

    }
}
