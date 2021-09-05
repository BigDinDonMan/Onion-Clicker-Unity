using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    public static UIActions instance;

    public float popUpHideTime;
    private WaitForSeconds popUpWaitTime;

    public GameObject popUpsParent;
    public StackedNotifications stackedNotifications;

    public GameObject achievementPopUpPrefab;
    public GameObject clickIncomeTextPrefab;
    public GameObject savePopUpPrefab;
    public GameObject generatorUpgradeDetailsWindowPrefab;

    public GameObject upgradesBuyParent;

    public Canvas canvas;

    private Camera gameCamera;

    [SerializeField]
    private SavedStatePersistor statePersistor;

    private void Awake() {
        instance = this;
        gameCamera = Camera.main;
        stackedNotifications = popUpsParent.GetComponent<StackedNotifications>();
        popUpWaitTime = new WaitForSeconds(popUpHideTime);
    }

    public void SpawnTextOnClick() {
        var clickPosition = Input.mousePosition;
        var worldPosition = gameCamera.ScreenToWorldPoint(clickPosition);
        var textObject = Instantiate(clickIncomeTextPrefab, new Vector3(worldPosition.x, worldPosition.y, 0f), Quaternion.identity, canvas.transform);
        var glidingText = textObject.GetComponent<GlidingText>();
        glidingText.text.text = $"+{PlayerDetails.instance.ClickIncome}";
    }

    public void SpawnAchievementPopUp(Achievement achievement) {
        var popUp = Instantiate(achievementPopUpPrefab);
        stackedNotifications.AddNotification(popUp);
        var nameAndImageParent = popUp.transform.Find("NameAndImageParent");
        var image = nameAndImageParent.GetComponentInChildren<Image>();
        var text = nameAndImageParent.GetComponentInChildren<TextMeshProUGUI>();
        image.sprite = achievement.achievementIcon;
        text.text = achievement.achievementName;
        StartCoroutine(HideNotification(popUp));
    }

    public void SpawnSavePopUp() {
        var popUp = Instantiate(savePopUpPrefab);
        stackedNotifications.AddNotification(popUp);
        StartCoroutine(HideNotification(popUp));
    }

    public void SpawnGeneratorUpgradeDetailsWindow(GameUpgrade upgrade) {
        var upgradeDetails = Instantiate(generatorUpgradeDetailsWindowPrefab, upgradesBuyParent.transform);
        upgradeDetails.GetComponent<BuyUpgradeWindow>().SetUpgrade(upgrade);
    }

    public void ForceSaveGameState() {
        statePersistor.StopCoroutine("SaveCurrentGameState");

        statePersistor.PersistData();
        SpawnSavePopUp();

        statePersistor.StartSaving();
    }

    public void ShowStatisticsWindow() {

    }

    public void ShowSettingsWindow() {

    }

    public void ShowAchievementsWindow() {

    }

    private IEnumerator HideNotification(GameObject popup) {
        yield return popUpWaitTime;
        stackedNotifications.RemoveNotification(popup);
    }
}
