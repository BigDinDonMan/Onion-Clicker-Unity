using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    public static UIActions instance;

    public float popUpHideTime;

    public GameObject popUpsParent;
    public StackedNotifications stackedNotifications;

    public GameObject achievementPopUpPrefab;
    public GameObject clickIncomeTextPrefab;
    public GameObject savePopUpPrefab;
    public GameObject generatorUpgradeDetailsWindowPrefab;

    public Canvas canvas;

    private Camera gameCamera;

    private void Awake() {
        instance = this;
        gameCamera = Camera.main;
        stackedNotifications = popUpsParent.GetComponent<StackedNotifications>();
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
        StartCoroutine(HideAchievementPopUp(popUp));
    }

    public void SpawnSavePopUp() {

    }

    public void SpawnGeneratorUpgradeDetailsWindow(GameUpgrade upgrade) {

    }

    private IEnumerator HideAchievementPopUp(GameObject popup) {
        yield return new WaitForSeconds(popUpHideTime);
        stackedNotifications.RemoveNotification(popup);
    }
}
