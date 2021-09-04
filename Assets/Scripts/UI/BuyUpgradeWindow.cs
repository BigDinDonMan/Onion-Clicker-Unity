using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUpgradeWindow : MonoBehaviour
{
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI flavorText;
    public TextMeshProUGUI description;
    public TextMeshProUGUI price;
    public Image icon;
    public Button buyButton;
    public GameUpgrade upgrade;

    private PlayerDetails playerDetails;

    private void Awake() {
        buyButton.onClick.AddListener(this.Buy);
    }

    private void Start() {
        playerDetails = PlayerDetails.instance;
        UpdateBuyButtonInteractability();
    }

    public void SetUpgrade(GameUpgrade u) {
        upgrade = u;

        upgradeName.text = u.upgradeName;
        icon.sprite = upgrade.upgradeIcon;
        flavorText.text = $"\"{u.flavorText}\"";
        description.text = u.description;
        price.text = $"Buy ({u.upgradeCost} onions)"; //todo: change that later to use a suffix like millions, billions etc.
    }

    private void Update() {
        UpdateBuyButtonInteractability();
    }

    private void UpdateBuyButtonInteractability() {
        buyButton.interactable = playerDetails.Onions >= upgrade.upgradeCost;
    }

    public void Buy() {
        Destroy(this.gameObject);
        UpgradesManager.instance.Buy(upgrade);
        playerDetails.ChangeOnions(-upgrade.upgradeCost);
    }
}
