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
        numberInfo = new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberGroupSeparator = "." };
    }

    public string MapToString(double incomeNum) {
        string stringifiedNum = incomeNum.ToString("n0", numberInfo);
        int actualLength = stringifiedNum.Count(char.IsDigit);
        var entry = entries.Find(e => actualLength <= e.upperRangeBound && actualLength >= e.lowerRangeBound);
        if (entry == null) return "?";
        int count = 0;
        int index = 0;
        char sep = numberInfo.NumberGroupSeparator[0];
        foreach (var (c, i) in stringifiedNum.Enumerate()) {
            if (c == sep) {
                count++;
                index = i;
            }
            if (count >= 2) break;
        }
        return $"{stringifiedNum.Substring(0, index <= 0 ? actualLength : index)} {entry.suffix}".Trim();
    }
}
