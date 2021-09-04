using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetails : MonoBehaviour
{
    public static PlayerDetails instance;

    [SerializeField]
    private double onions;
    public double Onions { get => onions; }

    public event System.Action OnOnionsChanged;

    [SerializeField]
    private double clickIncome = 1d;
    public double ClickIncome { get => clickIncome; }

    public void IncreaseClickIncome(double multiplier) {
        clickIncome *= multiplier;
    }

    [SerializeField]
    private double incomeMultiplier = 1d; //this should not be multiplied; only added to
    public double GlobalIncomeMultiplier { get => incomeMultiplier; }

    public ulong TotalClicks = 0;

    public double TotalOnionsEarned = 0d;
    public double TotalOnionsSpent = 0d;
    public double TotalOnionsClicked = 0d;

    public void IncreaseMultiplier(double diff) {
        incomeMultiplier += diff;
    }

    private void Awake() {
        instance = this;
    }

    public void AddClickIncome() {
        ChangeOnions(clickIncome);
        TotalOnionsClicked += clickIncome;
        TotalClicks++;
    }

    public void ChangeOnions(double change) {
        onions += change;
        if (change < 0) {
            TotalOnionsSpent += System.Math.Abs(change);
        } else {
            TotalOnionsEarned += change;
        }
        OnOnionsChanged?.Invoke();
    }
}
