using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put all achievement data here and every achievement type will pull out whatever it needs
[System.Serializable]
public struct AchievementTriggerData
{
    public double totalOnions;
    public uint generatorAmount;
    public uint totalGeneratorAmount;
    public ulong totalClicks;
    public double totalClickOnions;
    public Object payload;
}
