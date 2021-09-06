using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class storing the number ranges and a corresponding name, e.g. million or billion
[System.Serializable]
public class IncomeSuffixEntry
{
    public string suffix;
    public short lowerRangeBound;
    public short upperRangeBound;
}
