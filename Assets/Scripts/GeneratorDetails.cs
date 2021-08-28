using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//todo: add a function that will simulate the cost of buying X (1, 10, 100) generators and will grey out or enable buttons as needed
//todo: make an algorithm to increase the prices after buying generators
public class GeneratorDetails : MonoBehaviour
{
    public OnionGenerator generator;
    public int generatorAmount;
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


    private void Awake() {
        RecalculatePrices();
        incomePerGenerator = generator.baseIncome;
        UpdateGeneratorUI();
        buy1Button.onClick.AddListener(() => Buy());
        buy10Button.onClick.AddListener(() => Buy(10));
        buy100Button.onClick.AddListener(() => Buy(100));
    }

    public void Buy(int amount = 1) {

        void HandleBuy(double currentPrice) {
            if (PlayerDetails.instance.Onions < currentPrice) return;

            PlayerDetails.instance.ChangeOnions(-currentPrice);
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
    }

    public void UpdateButtonsEnabled() {
        var onions = PlayerDetails.instance.Onions;
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
        int owned = generatorAmount;
        for (int i = 0; i < entries; ++i, ++owned) {
            price += generator.basePrice;
        }
        return price;
    }

    public void RecalculatePrices() {
        currentGeneratorPrice = (ulong)CalculatePrice(1);
        current10GeneratorsPrice = (ulong)CalculatePrice(10);
        current100GeneratorsPrice = (ulong)CalculatePrice(100);
    }
}
