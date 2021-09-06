using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Globalization;

public class IncomeSuffixMap : MonoBehaviour
{
    public static IncomeSuffixMap instance;

    public List<IncomeSuffixEntry> entries;
    private NumberFormatInfo numberInfo;

    private void Awake() {
        instance = this;
        numberInfo = NumberFormatInfo.CurrentInfo;
    }

    public string MapToString(double incomeNum) {
        string stringifiedNum = $"{incomeNum:n0}";
        int actualLength = stringifiedNum.Count(char.IsDigit);
        var entry = entries.Find(e => actualLength <= e.upperRangeBound && actualLength >= e.lowerRangeBound);
        if (entry == null) return "?";
        char separator = numberInfo.NumberGroupSeparator[0];
        int sepIndex = stringifiedNum.IndexOf(separator);
        return (sepIndex != -1 ? $"{stringifiedNum.Substring(0, sepIndex)} {entry.suffix}" : stringifiedNum).Trim();
    }
}
