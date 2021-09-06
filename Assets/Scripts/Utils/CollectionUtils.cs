using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class CollectionUtils
{
    public static IEnumerable<(T, int)> Enumerate<T>(this IEnumerable<T> collection, int start = 0) {
        int index = start;
        foreach (var item in collection) {
            yield return (item, index++);
        }
    }
}
