using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIActions : MonoBehaviour
{
    public static UIActions instance;

    public GameObject clickIncomeTextPrefab;
    public Canvas canvas;

    public void SpawnTextOnClick() {
        var clickPosition = Input.mousePosition;
        var textObject = Instantiate(clickIncomeTextPrefab, clickPosition, Quaternion.identity, canvas.transform);
        var glidingText = textObject.GetComponent<GlidingText>();
        glidingText.text.text = PlayerDetails.instance.ClickIncome.ToString();
    }
}
