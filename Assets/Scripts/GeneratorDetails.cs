using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//todo: add a function that will simulate the cost of buying X (1, 10, 100) generators and will grey out or enable buttons as needed
//todo: make an algorithm to increase the prices after buying generators
public class GeneratorDetails : MonoBehaviour
{
    public OnionGenerator generator;
    public uint generatorAmount;
    public ulong currentGeneratorPrice;
    public ulong current10GeneratorsPrice;
    public ulong current100GeneratorsPrice;
    public double incomePerGenerator;

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


    private void Awake() {
        playerDetails = PlayerDetails.instance;
        RecalculatePrices();
        incomePerGenerator = generator.baseIncome;
        UpdateGeneratorUI();
        buy1Button.onClick.AddListener(() => Buy());
        buy10Button.onClick.AddListener(() => Buy(10));
        buy100Button.onClick.AddListener(() => Buy(100));
    }

    public void Buy(uint amount = 1) {

        void HandleBuy(double currentPrice) {
            if (playerDetails.Onions < currentPrice) return;

            playerDetails.ChangeOnions(-currentPrice);
            generatorAmount += amount;
            currentGeneratorPrice = (ulong)CalculatePrice(1);
            current10GeneratorsPrice = (ulong)CalculatePrice(10);
            current100GeneratorsPrice = (ulong)CalculatePrice(100);
            UpdateGeneratorUI();
        }

        HandleBuy(amount switch {
            1 => currentGeneratorPrice,
            10 => current10GeneratorsPrice,
            100 => current100GeneratorsPrice,
            _ => 0
        });

        TryUnlockAchievements();
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
        generatorNameText.text = generator.name;
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
        //todo: maybe do that in a loop so that we unlock all of the suitable achievements along the way
        var unlockableAchievements = generatorAchievements.Where(a => a.unlocked == false);
        var triggerData = new AchievementTriggerData() { 
            generator = this.generator,
            generatorAmount = this.generatorAmount
        };
        foreach (var achievement in unlockableAchievements) {
            var unlockSuccess = achievement.Unlock(triggerData);
            if (unlockSuccess) {
                //todo: show achievement popup here
                //todo: also create a sliding animation for these bad boys
                Debug.Log($"Unlocked achievement: {achievement.achievementName}, at: {achievement.unlockedAt}");
                UIActions.instance.SpawnAchievementPopUp(achievement);
            }
        }
    }
}
