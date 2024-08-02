using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ExtensionMethods
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<object> source)
    {
        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

        foreach (object item in source)
        {
            FieldInfo[] fields = item.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            if (fields.Length < 2)
            {
                throw new InvalidOperationException("The class must have at least two public fields.");
            }

            // Check if the first field type is the same as the specified TKey
            if (fields[0].FieldType == typeof(TKey))
            {
                TKey key = (TKey)fields[0].GetValue(item);
                TValue value = (TValue)fields[1].GetValue(item);
                dict.Add(key, value);
            }
            else
            {
                throw new InvalidOperationException("The first field must be of type " + typeof(TKey).Name);
            }
        }

        return dict;
    }
}