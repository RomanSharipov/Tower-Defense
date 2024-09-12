using System;
using System.Collections.Generic;
using System.Reflection;

public static class DictionaryExtensionMethods
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<object> source) where TKey : Enum
    {
        Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

        foreach (object item in source)
        {
            FieldInfo[] fields = item.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            if (fields.Length < 2)
            {
                throw new InvalidOperationException("The class must have at least two public fields.");
            }

            if (fields[0].FieldType.IsEnum)
            {
                TKey key = (TKey)fields[0].GetValue(item);
                TValue value = (TValue)fields[1].GetValue(item);
                dict.Add(key, value);
            }
            else
            {
                throw new InvalidOperationException("The first field must be of enum type.");
            }
        }

        return dict;
    }

    public static Dictionary<string, TValue> ToDictionary<TValue>(this IEnumerable<object> source)
    {
        Dictionary<string, TValue> dict = new Dictionary<string, TValue>();

        foreach (object item in source)
        {
            FieldInfo[] fields = item.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            if (fields.Length < 2)
            {
                throw new InvalidOperationException("The class must have at least two public fields.");
            }

            if (fields[0].FieldType == typeof(string))
            {
                string key = (string)fields[0].GetValue(item);
                TValue value = (TValue)fields[1].GetValue(item);
                dict.Add(key, value);
            }
            else
            {
                throw new InvalidOperationException("The first field must be of enum type.");
            }
        }

        return dict;
    }
}