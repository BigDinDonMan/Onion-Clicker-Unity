using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Income/Generators", fileName = "New Income Generator")]
public class OnionGenerator : ScriptableObject
{
    public string generatorName;
    public string description;
    public int basePrice;
    public double baseIncome;
    public double coefficient;
    public Sprite generatorIcon;
}
