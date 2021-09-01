using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUpgradeWindow : MonoBehaviour
{
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI flavorText;
    public TextMeshProUGUI description;
    public Image icon;
    public Button buyButton;
    public GameUpgrade upgrade;

    private PlayerDetails playerDetails;

    private void Awake() {
        buyButton.onClick.AddListener(this.Buy);
    }

    private void Start() {
        playerDetails = PlayerDetails.instance;
    }

    public void SetUpgrade(GameUpgrade u) {
        upgrade = u;

        upgradeName.text = u.upgradeName;
        icon.sprite = upgrade.upgradeIcon;
        flavorText.text = u.flavorText;
        description.text = u.description;
    }

    private void Update() {
        UpdateBuyButtonInteractability();
    }

    private void UpdateBuyButtonInteractability() {
        buyButton.interactable = playerDetails.Onions >= upgrade.upgradeCost;
    }

    public void Buy() {
        Destroy(this.gameObject);
    }
}
