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
    private int clickIncome;
    public int ClickIncome { get => clickIncome; }

    [SerializeField]
    private double incomeMultiplier = 1d;
    public double GlobalIncomeMultiplier { get => incomeMultiplier; }

    public double TotalOnionsEarned = 0d;
    public double TotalOnionsSpent = 0d;

    public void IncreaseMultiplier(double diff) {
        incomeMultiplier += diff;
    }

    private void Awake() {
        instance = this;
    }

    public void AddClickIncome() {
        ChangeOnions(clickIncome);
        OnOnionsChanged?.Invoke();
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
