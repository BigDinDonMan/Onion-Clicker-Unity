using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put all achievement data here and every achievement type will pull out whatever it needs
[System.Serializable]
public struct AchievementTriggerData //mark unused values with -1 or a very large number
{
    public double totalOnions;
    public uint generatorAmount;
    public uint totalGeneratorAmount;
    public ulong totalClicks;
    public double totalClickOnions;
    public OnionGenerator generator;
}
