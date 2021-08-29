using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIActions : MonoBehaviour
{
    public static UIActions instance;

    public float popUpHideTime;

    public GameObject achievementPopUpsParent;
    public StackedNotifications stackedNotifications;

    public GameObject achievementPopUpPrefab;
    public GameObject clickIncomeTextPrefab;
    public Canvas canvas;

    private void Awake() {
        instance = this;
        stackedNotifications = achievementPopUpsParent.GetComponent<StackedNotifications>();
    }

    public void SpawnTextOnClick() {
        var clickPosition = Input.mousePosition;
        var textObject = Instantiate(clickIncomeTextPrefab, clickPosition, Quaternion.identity, canvas.transform);
        var glidingText = textObject.GetComponent<GlidingText>();
        glidingText.text.text = $"+{PlayerDetails.instance.ClickIncome}";
    }

    public void SpawnAchievementPopUp(Achievement achievement) {
        var popUp = Instantiate(achievementPopUpPrefab, achievementPopUpsParent.transform);
        StartCoroutine(HideAchievementPopUp(popUp));
    }

    private IEnumerator HideAchievementPopUp(GameObject popup) {
        yield return new WaitForSeconds(popUpHideTime);
        Destroy(popup);
    }
}
