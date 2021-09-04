using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorDetails : MonoBehaviour
{
    public OnionGenerator generator;
    public uint generatorAmount;
    public ulong currentGeneratorPrice;
    public ulong current10GeneratorsPrice;
    public ulong current100GeneratorsPrice;
    public double incomePerGenerator;
    public double incomeGeneratorMultiplier = 1d; //basic global generator multiplier
    public double increaseFromOtherGeneratorsMultiplier = 0d; //this will be recalculated after every change of generatorAmount after buying these stupid upgrades
    public double TotalMultiplier { get => /*addedGeneratorMultiplier + */incomeGeneratorMultiplier + increaseFromOtherGeneratorsMultiplier; }

    public Button buy1Button;
    public TextMeshProUGUI buy1ButtonText;
    public Button buy10Button;
    public TextMeshProUGUI buy10ButtonText;
    public Button buy100Button;
    public TextMeshProUGUI buy100ButtonText;
    public Image generatorImage;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI generatorNameText;

    private PlayerDetails playerDetails;

    public List<Achievement> generatorAchievements;
    public List<GameUpgrade> generatorUpgrades;

    private event System.Action OnBuy;
    public event System.Action<uint, uint> OnGeneratorAmountChanged;
    //first argument is a generator to increase, second argument is current generator amount (N) by which it will increase the first argument's multiplier
    //yes i speak veri gud ingrish 
    //public event System.Action<OnionGenerator, uint> IncreasePerN_OnGeneratorAmountChanged; 

    private void Start() {
        playerDetails = PlayerDetails.instance;
        RecalculatePrices();
        incomePerGenerator = generator.baseIncome;
        UpdateGeneratorUI();
        buy1Button.onClick.AddListener(() => Buy());
        buy10Button.onClick.AddListener(() => Buy(10));
        buy100Button.onClick.AddListener(() => Buy(100));
        OnBuy += this.TryUnlockAchievements;
        OnBuy += this.TryUnlockUpgrades;
        OnBuy += this.UpdateGeneratorUI;
    }

    public void Buy(uint amount = 1) {

        void HandleBuy(double currentPrice) {
            if (playerDetails.Onions < currentPrice) return;

            playerDetails.ChangeOnions(-currentPrice);
            uint oldAmount = generatorAmount;
            generatorAmount += amount;
            OnGeneratorAmountChanged?.Invoke(oldAmount, generatorAmount);
            currentGeneratorPrice = (ulong)CalculatePrice(1);
            current10GeneratorsPrice = (ulong)CalculatePrice(10);
            current100GeneratorsPrice = (ulong)CalculatePrice(100);
        }

        HandleBuy(amount switch {
            1 => currentGeneratorPrice,
            10 => current10GeneratorsPrice,
            100 => current100GeneratorsPrice,
            _ => 0
        });

        OnBuy?.Invoke();
    }

    public void UpdateButtonsEnabled() {
        var onions = playerDetails.Onions;
        SetButtonEnabled(buy1Button, onions >= currentGeneratorPrice);
        SetButtonEnabled(buy10Button, onions >= current10GeneratorsPrice);
        SetButtonEnabled(buy100Button, onions >= current100GeneratorsPrice);
    }

    public void SetButtonEnabled(Button b, bool enabled) => b.interactable = enabled;

    public void UpdateGeneratorUI() {
        if (generator == null) return;
        amountText.text = $"{generatorAmount}x";
        generatorNameText.text = generator.generatorName;
        generatorImage.sprite = generator.generatorIcon;
        buy1ButtonText.text = $"Buy 1\n({currentGeneratorPrice} onions)";
        buy10ButtonText.text = $"Buy 10\n({current10GeneratorsPrice} onions)";
        buy100ButtonText.text = $"Buy 100\n({current100GeneratorsPrice} onions)";
    }

    public double CalculatePrice(int entries = 1) {
        double price = 0d;
        int owned = (int)generatorAmount;
        for (int i = 0; i < entries; ++i, ++owned) {
            price += generator.basePrice * System.Math.Pow(generator.coefficient, owned);
        }
        return price;
    }

    public void RecalculatePrices() {
        currentGeneratorPrice = (ulong)CalculatePrice(1);
        current10GeneratorsPrice = (ulong)CalculatePrice(10);
        current100GeneratorsPrice = (ulong)CalculatePrice(100);
    }

    public void TryUnlockAchievements() {
        var unlockableAchievements = generatorAchievements.Where(a => AchievementManager.instance.IsUnlocked(a) == false);
        var triggerData = new AchievementTriggerData() { 
            generator = this.generator,
            generatorAmount = this.generatorAmount
        };
        foreach (var achievement in unlockableAchievements) {
            var unlockSuccess = achievement.Unlock(triggerData);
            if (unlockSuccess) {
                //todo: also create a sliding animation for these bad boys
                UIActions.instance.SpawnAchievementPopUp(achievement);
            }
        }
    }

    public void TryUnlockUpgrades() {
        var unlockableUpgrades = generatorUpgrades.Where(u => UpgradesManager.instance.IsUnlocked(u) == false);
        foreach (var upgrade in unlockableUpgrades) {
            if (generatorAmount >= upgrade.unlockedAtGenerators) {
                UpgradesManager.instance.Unlock(upgrade);
            }
        }
    }
}
